using ShippingLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/*      CSE 598 - Assignment 5&6
 *                An example shopping Web Application
 *     Group 43 - Mark Adan, James Cajas, Andrew Munroe
 *      Version - 1.0
 *  Last Update - 11/11/25
 *  
 * 
 */

namespace Group43
{
    public partial class Shopping : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string userName;

            if (Session["userName"] == null)
            {
                userLabel.Text = "You are logged in as Guest.";
            }
            else
            {
                userName = Session["userName"].ToString();
                userLabel.Text = "You are logged in as " + userName; 
            }
        }
        // Search_Btn_Click - calls the the eBay serice
        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            string results = string.Empty;
            List<string> parsedItems;
            string[] item1 = { };
            string[] item2 = { };
            string[] item3 = { };

            // Create client
            MyEbayServiceReference.MyEbayService1Client proxy = new MyEbayServiceReference.MyEbayService1Client();

            try
            {
                // call MyEbay service operation
                // returns a string with 3 text arrays
                // with [Title, Price, and Item Number]
                results = proxy.SearchItem(SearchTxtBox.Text);

                if (results == "No items found.")
                {
                    noItemsLbl.Visible = true;
                    table1.Visible = false;
                    cartLabel.Visible = false;
                    //estimatorLbl.Visible = false;
                    //stateLbl.Visible = false;
                    //stateTxtbox.Visible = false;
                    //submitBtn.Visible = false;
                    //shippingCostLbl.Visible = false;
                }
                else
                {
                    // Parse individual results
                    parsedItems = GetItems(results, '[', ']');

                    Console.WriteLine(parsedItems);

                    // Split items into description, price, id array
                    item1 = parsedItems[0].Split(',');
                    item2 = parsedItems[1].Split(',');
                    item3 = parsedItems[2].Split(',');

                    // Item 1
                    parsedItems = GetItems(item1[2], '|', '|');
                    r1Id.Text = parsedItems[0];
                    r1Desc.Text = item1[0];
                    r1Price.Text = item1[1];

                    // Item 2
                    parsedItems = GetItems(item2[2], '|', '|');
                    r2Id.Text = parsedItems[0];
                    r2Desc.Text = item2[0];
                    r2Price.Text = item2[1];

                    // Item 3
                    parsedItems = GetItems(item3[2], '|', '|');
                    r3Id.Text = parsedItems[0];
                    r3Desc.Text = item3[0];
                    r3Price.Text = item3[1];

                    table1.Visible = true;
                    noItemsLbl.Visible = false;
                    cartLabel.Visible = true;

                    // Retrieve the cart cookie
                    HttpCookie cartCookie = Request.Cookies["ShoppingCart"];

                    if ((cartCookie != null) && (cartCookie.Value.Split('|').Length > 0))
                    {
                        cartLabel.Text = "Items in cart: " + cartCookie.Value.Split('|').Length;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: getting items - {}", ex);
            }
            finally
            {
                proxy.Close();
            }
            
        }

        // AddItemBtn1_Click - add item 1 to cart
        protected void AddItemBtn1_Click(object sender, EventArgs e)
        {
            WordCountSvc.Service1Client proxy = new WordCountSvc.Service1Client();

            try
            {
                // Remove Stop Words from Description using WordCountSvc
                string filteredDesc = proxy.FilterContent(r1Desc.Text);

                string id = r1Id.Text;
                string item = TrimDescription(filteredDesc);  // Trim the Description for Shopping Cart
                string price = r1Price.Text;

                // add item to shopping cart cookie
                if (item.Length != 0 && price.Length != 0 && id.Length != 0)
                    UpdateShoppingCart(id, item, price);
            }
            catch (Exception ex)
            {

                Console.WriteLine("ERROR: adding item 1 - {}", ex);
            }
            finally
            {
                proxy.Close();
            }
        }

        // AddItemBtn2_Click - add item 3 to cart
        protected void AddItemBtn2_Click(object sender, EventArgs e)
        {
            WordCountSvc.Service1Client proxy = new WordCountSvc.Service1Client();

            try
            {
                // Remove Stop Words from Description using WordCountSvc
                string filteredDesc = proxy.FilterContent(r2Desc.Text);

                string id = r2Id.Text;
                string item = TrimDescription(filteredDesc); // Trim the Description for Shopping Cart
                string price = r2Price.Text;

                // add item to shopping cart cookie
                if (item.Length != 0 && price.Length != 0 && id.Length != 0)
                    UpdateShoppingCart(id, item, price);
            }
            catch (Exception ex)
            {

                Console.WriteLine("ERROR: adding item 3 - {}", ex);
            }
            finally
            {
                proxy.Close();
            }
        }

        // AddItemBtn3_Click - add item 3 to cart
        protected void AddItemBtn3_Click(object sender, EventArgs e)
        {
            WordCountSvc.Service1Client proxy = new WordCountSvc.Service1Client();

            try
            {
                // Remove Stop Words from Description using WordCountSvc
                string filteredDesc = proxy.FilterContent(r3Desc.Text);

                string id = r3Id.Text;
                string item = TrimDescription(filteredDesc); // Trim the Description for Shopping Cart
                string price = r3Price.Text;

                // add item to shopping cart cookie
                if (item.Length != 0 && price.Length != 0 && id.Length != 0)
                    UpdateShoppingCart(id, item, price);
            }
            catch (Exception ex)
            {

                Console.WriteLine("ERROR: adding item 3 - {}", ex);
            }
            finally
            {
                proxy.Close();
            }
        }

        // HomeBtn_Click - home page navigation
        protected void HomeBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        // ShoppingCartBtn_Click - check out page navigation
        protected void ShoppingCartBtn_Click(object sender, EventArgs e)
        {
            // go to shopping cart
            Response.Redirect("ShoppingCart.aspx");
        }

        // UpdateShoppingCart - adds an item to a ShoppingCart cookie
        //                      to be read at "Checkout"
        private void UpdateShoppingCart(string id, string item, string price)
        {
            // Create or retrieve the cart cookie
            HttpCookie cartCookie = Request.Cookies["ShoppingCart"];

            if (cartCookie == null)
            {
                // If no cart cookie exists, create a new one
                cartCookie = new HttpCookie("ShoppingCart");
                cartCookie.Expires = DateTime.Now.AddDays(7); // Set expiration for persistent cart
            }

            // Add or update the product in the cookie
            string newItem = $"{id},{item},{price}";

            if (cartCookie.Value == null)
            {
                cartCookie.Value = newItem;
            }
            else
            {
                // Append new item with a separator
                cartCookie.Value += "|" + newItem; 
            }

            // Update the cookie
            Response.Cookies.Add(cartCookie);

            // Update cart label
            cartLabel.Text = "Items in cart: " + cartCookie.Value.Split('|').Length;
        }

        // TrimDescription - Trims the item description since 
        //                   some titles are long
        private string TrimDescription(string itemDescription)
        {

            if (string.IsNullOrEmpty(itemDescription))
            {
                return string.Empty;
            }

            // Split the item description into words based on whitespace
            string[] words = itemDescription.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            // Take the first 5 words or fewer if the string has less than 5 words
            string[] firstFive = words.Take(5).ToArray();

            // Join the selected words back into a single string
            return string.Join(" ", firstFive);
 
        }

        // GetItems - Extracts a substring between two separators
        //            e.g. [ID, Description, Price]
        private List<string> GetItems(string results, char startChar, char endChar)
        {
            int startIndex = 0;
            List<string> items = new List<string>();

            while (true)
            {
                // Find index of start character, starting from the current position
                int firstCharIndex = results.IndexOf(startChar, startIndex);
                if (firstCharIndex == -1) // No more start characters found
                {
                    break;
                }

                // Find index end character, starting *after* the start character
                int secondCharIndex = results.IndexOf(endChar, firstCharIndex + 1);
                if (secondCharIndex == -1) // No matching end character found
                {
                    break;
                }

                // Extract the substring between the two characters
                int substringStart = firstCharIndex + 1; // Exclude the start character
                int substringLength = secondCharIndex - substringStart;
                items.Add(results.Substring(substringStart, substringLength));

                // Update startIndex to continue searching after the current end character
                startIndex = secondCharIndex + 1;
            }

            return items;
        }

        // SubmitBtn_Click - Shipping Estimator submit button
        //                   returns a shipping price based on region
        protected void SubmitBtn_Click(object sender, EventArgs e)
        {
            if (stateTxtbox.Text.Length > 0)
            {
                double cost = Shipping.ShippingEstimator(stateTxtbox.Text);
                shippingCostLbl.Text = "Estimated Shipping to " + stateTxtbox.Text 
                    + ": $" + cost.ToString();
            }
        }
    }
}