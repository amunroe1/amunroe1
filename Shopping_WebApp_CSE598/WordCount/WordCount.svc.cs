using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Web;

namespace WordCount
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class WordCount : IService1
    {
 
        public string GetWordCount(string content)
        {  

            if (string.IsNullOrEmpty(content))
            {
                return "The input is empty.";
            }

            // Pass the content to the Word Filtering service to remove XML tags and stop words
            content = FilterContent(content);

            // Split the content into words using delimeters
            char[] delimiters = new char[] { ' ', '\r', '\n', '\t', '.', ',', ';', '!', '?', '-', '_', '\"', '\'', '(', ')', '[', ']', '{', '}', '<', '>' };
            string[] words = content.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            // Call the CountWords method to get word counts
            Dictionary<string, int> wordCounts = CountWords(words);
            string result = JsonSerializer.Serialize(wordCounts);
            return result;

        }

        private Dictionary<string, int> CountWords(string [] words)
        {
            var wordCounts = new Dictionary<string, int>();

            // Count occurrences of each word
            foreach (var word in words)
            {
                if (wordCounts.ContainsKey(word.ToLower()))
                {
                    wordCounts[word.ToLower()]++;
                }
                else
                {
                    wordCounts[word.ToLower()] = 1;
                }
            }
            return wordCounts;
        }

        public string FilterContent(string input)
        {
            
            //List of stop words to be filtered out
            List<string> stopWords = new List<string>
            {
                "i", "me", "my", "myself", "we", "our", "ours", "ourselves", "you", "your", "yours", "yourself", "yourselves", "he", "him", "his", "himself", "she", "her", "hers", "herself", "it", "its", "itself", "they", "them", "their", "theirs", "themselves", "what", "which", "who", "whom", "this", "that", "these", "those", "am", "is", "are", "was", "were", "be", "been", "being", "have", "has", "had", "having", "do", "does", "did", "doing", "a", "an", "the", "and", "but", "if", "or", "because", "as", "until", "while", "of", "at", "by", "for", "with", "about", "against", "between", "into", "through", "during", "before", "after", "above", "below", "to", "from", "up", "down", "in", "out", "on", "off", "over", "under", "again", "further", "then", "once", "here", "there", "when", "where", "why", "how", "all", "any", "both", "each", "few", "more", "most", "other", "some", "such", "no", "nor", "not", "only", "own", "same", "so", "than", "too", "very", "s", "t", "can", "will", "just", "don", "should", "now"
            };

            // Remove XML tags from each part
            input = System.Text.RegularExpressions.Regex.Replace(input, "<[^>]+>", " ");

            // Split the input string at underscores
            string[] parts = input.Split();



            List<string> stopWordFilt = new List<string>();
            foreach (string word in parts)
            {
                if (!stopWords.Contains(word.ToLower()))
                {
                    stopWordFilt.Add(word.Trim());
                }
            }

            // Join the cleaned parts back together with spaces (or underscores if you prefer)
            var filtered = Regex.Replace(string.Join(" ", stopWordFilt), @"\s+", " ").Trim();
            return filtered;
        }
    }
}
