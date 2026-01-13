using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text.Json;

/**************************************************
 *      CSE 598 - Assignment 3 Ex. 2.2 
 *              - An Implementation for a WSDL service
 *                acting as a wrapper for eBay's 
 *                Search and GetItem API's
 *         
 *       author - Mark Adan
 *      version - 1.0
 * Last updated - 10/10/25
 **************************************************/

namespace MyEbayService
{
    // The eBay search response
    public class EbayResult
    {
        public ItemSummary[] itemSummaries { get; set; }
        public string href { get; set; }
        public int total { get; set; }
        public string next { get; set; }
        public int limit { get; set; }
        public int offset { get; set; }
    }

    // A single eBay item summary
    public class ItemSummary
    {
        public string itemId { get; set; }
        public string title { get; set; }
        public string itemGroupHref { get; set; }
        public int leafCategoryId { get; set; }
        public Category[] categories { get; set; }
        public Image image { get; set; }
        public Price price { get; set; }
        public string itemHref { get; set; }
        public Seller seller { get; set; }
        public string condition { get; set; }
        public string conditionId { get; set; }
        public string shortDescription { get; set; }
    }

    // The eBay category summary
    public class Category
    {
        public string categoryId { get; set; }
        public string categoryName { get; set; }
    }

    // The eBay image summary
    public class Image
    {
        public string imageUrl { get; set; }
    }

    // The eBay price details
    public class Price
    {
        public string value { get; set; }
        public string currency { get; set; }
    }

    // The eBay seller details
    public class Seller
    {
        public string userName { get; set; }
        public string feedbackPercentage { get; set; }
        public int feedbackScore { get; set; }
    }

    // The eBay Access Token details
    public class Token
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string token_type { get; set; }
    }

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MyEbayService1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select MyEbayService1.svc or MyEbayService1.svc.cs at the Solution Explorer and start debugging.
    public class MyEbayService1 : IMyEbayService1
    {
        // Sandbox (for testing):
        
        //private const string baseUrl = "https://api.sandbox.ebay.com/buy/browse/v1/item_summary/search";  

        // Production: 
        
        private const string baseUrl = "https://api.ebay.com/buy/browse/v1/";
        private const string tokenEndpoint = "https://api.ebay.com/identity/v1/oauth2/token";
        // Note: Replace "Fake" with your actual eBay Developer credentials

        // SearchItem - the search Operation for MyEbay web service
        // param: queryItem - a string of keyword(s) to search
        // return: string - 3 eBay Items by Title, Price, Item Number 
        public async Task<string> SearchItem(string queryItem)
        {
            // Get eBay access token like the RefreshToken shown above
            string accessToken = await GetAccessTokenAsync();

            // Search the eBay  market place
            string result = await SearchEbayAsync(queryItem, accessToken);
            return result;
        }

        // SearchItem - the search Operation for MyEbay web service
        // param: queryItem - a string of keyword(s) to search
        // return: string - 3 eBay Items by Title, Price, Item Number 
        public async Task<string> GetItem(string listingId)
        {
            // Get eBay access token like the RefreshToken shown above
            string accessToken = await GetAccessTokenAsync();

            string result = await GetEbayAsync(listingId, accessToken);

            return result;
        }

        // GetAccessTokenAsync - an asynchronous method for requesting access to eBay's API's
        // return: string - an access token for connecting to eBay's Browse APIs
        private async Task<string> GetAccessTokenAsync()
        {
            string response = null;

            // Request permission to access eBay's public API's
            string scope = "https://api.ebay.com/oauth/api_scope";

            // create a http client
            HttpClient httpClient = new HttpClient();

            // Encode developer id and password for basic authorization
            // These are obtained after registering applications at https://developer.ebay.com
            var credentials = $"{"Fake"}:{"Fake"}";
            var encodedCredentials = Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials));

            // Set http headers
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", encodedCredentials);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Prepare request body
            var requestBody = new Dictionary<string, string>
            {
                {"grant_type", "client_credentials"},
                {"scope", scope}
            };

            // Set the http body
            var content = new FormUrlEncodedContent(requestBody);

            try
            {
                // Make the POST request
                HttpResponseMessage responseMessage = await httpClient.PostAsync(tokenEndpoint, content);

                // Hopefully we've succeeded in the POST request
                if (responseMessage.IsSuccessStatusCode)
                {
                    response = await responseMessage.Content.ReadAsStringAsync();
                    //Console.WriteLine(response);

                    // Deserialize the response: field names and types need to be correct
                    Token token = JsonSerializer.Deserialize<Token>(response);
                    response = token.access_token;
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Request Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not get access token: {ex}");
            }

            return response;
        }


        // SearchEbayAsync - an asynchronous method for searching eBay's marketplace
        // param: item - something to search for
        // param: accessToken - an OAuth access token
        // return: string - listings from eBay
        private async Task<string> SearchEbayAsync(string item, string accessToken)
        {
            // eBay listings
            string resultString = "No items found.";

            if (item == null)
                return resultString;

            // Create Http client
            HttpClient httpClient = new HttpClient();

            // Set up request headers
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); //request JSON result
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            httpClient.DefaultRequestHeaders.Add("X-EBAY-C-MARKETPLACE-ID", "EBAY_US"); // US marketplace
            httpClient.DefaultRequestHeaders.Add("X-EBAY-API-AUTH-TOKEN", accessToken);

            // Construct the API URL with query parametrs
            var requestUrl = $"{baseUrl}item_summary/search?q={Uri.EscapeDataString(item)}&limit=3";     //Search for item, limit 3 results

            try
            {
                // Make the GET request: limit is 5000 per day
                HttpResponseMessage responseMessage = await httpClient.GetAsync(requestUrl);
                responseMessage.EnsureSuccessStatusCode();  // Throw an exception if the Http response status is an error code

                //Console.WriteLine("Status code: {responseMessage.StatusCode}");
                // Make async call and put result in variable response for later
                string responseBody = await responseMessage.Content.ReadAsStringAsync();

                // Deserialize the http response into the Ebay results object: field names
                // and types need to be correct
                EbayResult results = JsonSerializer.Deserialize<EbayResult>(responseBody);

                // Hopefully we found something
                if (results.itemSummaries != null)
                {
                    //sConsole.WriteLine($"Search results for '{item}':");

                    resultString = string.Empty;

                    // Results Basic formatting 
                    foreach (var x in results.itemSummaries)
                    {
                        string newTitle = x.title.Replace(',', ' ');
                        resultString += "[";
                        //Console.WriteLine($"- Title: {x.title}");
                        resultString += newTitle + ",";
                        //Console.WriteLine($"  Price: {x.price.value} {x.price.currency}");
                        resultString += x.price.value.ToString() + ",";
                        //Console.WriteLine($"Item id: {x.itemId}");
                        resultString += x.itemId.ToString() + "],";
                        //Console.WriteLine("---");
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Request Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occured: {ex.Message}");
            }

            return resultString;
        }

        // GetEbayAsync - an asynchronous method for getting items from eBay's marketplace
        // param: listingId - a real listing
        // param: accessToken - an OAuth access token
        // return: string - an actual eBay item, results depend on actual listingId
        private async Task<string> GetEbayAsync(string listingId, string accessToken)
        {
            // eBay item
            string resultString = "No item found.";
            //listingId = "177107842356";

            if (listingId == null)
                return resultString;

            // Create Http client
            HttpClient httpClient = new HttpClient();

            // Set up request headers
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); //request JSON result
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            httpClient.DefaultRequestHeaders.Add("X-EBAY-C-MARKETPLACE-ID", "EBAY_US"); // US marketplace
            httpClient.DefaultRequestHeaders.Add("X-EBAY-API-AUTH-TOKEN", accessToken);

            // Construct the API URL with query parametrs
            var requestUrl = $"{baseUrl}item/{Uri.EscapeDataString(listingId)}";

            try
            {
                // Make the GET request: limit is 5000 per day
                HttpResponseMessage responseMessage = await httpClient.GetAsync(requestUrl);
                responseMessage.EnsureSuccessStatusCode();  // Throw an exception if the Http response status is an error code

                //Console.WriteLine("Status code: {responseMessage.StatusCode}");
                // Make async call and put result in variable response for later
                string responseBody = await responseMessage.Content.ReadAsStringAsync();

                // Deserialize the http response into the Ebay ItemSummary object
                ItemSummary item = JsonSerializer.Deserialize<ItemSummary>(responseBody);

                // Format the results
                if (item != null)
                {
                    resultString = string.Empty;

                    resultString += item.itemId.ToString() +",";
                    resultString += item.title + ",";
                    resultString += item.shortDescription + ",";
                    resultString += item.price.value.ToString();
                }
                else
                {
                    resultString = "No item found;";
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Request Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occured: {ex.Message}");
            }

            return resultString;
        }
    }
}

  
   
