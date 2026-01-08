using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*     CSE 598 - Assigment 5 & 6
 *               ShippingDLL
 *     version - 1.0
 * las uppdate - 10/11/12
 * 
 */

namespace ShippingLibrary
{
    public class Shipping
    {
        public  enum Region
        {
            West,
            SouthWest,
            Midwest,
            SouthEast,
            NorthEast
        };

        //public Dictionary<String, String> states { get; } = new Dictionary<String, String>();
        //Note: Initializer List throws duplicate value exception
        /*{
            {"Maine", "NorthEast"}, {"ME", "NorthEast"},
            {"Vermont", "NorthEast"}, {"VT", "NorthEast"},
            {"New Hampshire", "NorthEast"}, {"NH", "NorthEast"},
            {"Massachusetts", "NorthEast"}, {"MA", "NorthEast"},
            {"Connecticut", "NorthEast"}, {"CT", "NorthEast"},
            {"Rhode Island", "NorthEast"}, {"RI", "NorthEast"},
            {"New York", "NorthEast"}, {"NY", "NorthEast"},
            {"New Jersey", "NorthEast"}, {"NJ", "NorthEast"},
            {"Pennsylvania", "NorthEast"}, {"PA", "NorthEast"},
            {"Delaware", "NorthEast"}, {"DE", "NorthEast"},
            {"Maryland", "NorthEast"}, {"MD", "NorthEast"},
            {"Washington D.C.", "SouthEast"}, {"DC", "SouthEast"},
            {"West Virginia", "SouthEast"}, {"WV", "SouthEast"},
            {"Virginia","SouthEast"}, {"VA", "SouthEast"},
            {"North Carolina", "SouthEast"}, {"NC", "SouthEast"},
            {"South Carolina", "SouthEast"}, {"SC", "SouthEast"},
            {"Georgia", "SouthEast"}, {"GA", "SouthEast"},
            {"Florida", "SouthEast"}, {"FL", "SouthEast"},
            {"Alabama", "SouthEast"}, {"AL", "SouthEast"},
            {"Tennessee", "SouthEast"}, {"TN", "SouthEast"},
            {"Kentucky", "SouthEast"}, {"KY", "SouthEast"},
            {"Missouri", "SouthEast"}, {"MO", "SouthEast"},
            {"Arkansas", "SouthEast"}, {"AR", "SouthEast"},
            {"Louisiana", "SouthEast"}, {"LA", "SouthEast"},
            {"North Dakota", "Midwest"}, {"ND", "Midwest"},
            {"South Dakota", "Midwest"},  {"SD", "Midwest"},
            {"Nebraska", "Midwest"}, {"NE", "Midwest"},
            {"Kansas", "Midwest"}, {"KS", "Midwest"},
            {"Minnesota", "Midwest"}, {"MN", "Midwest"},
            {"Iowa", "Midwest"}, {"IA", "Midwest"},
            {"Missouri", "Midwest"}, {"MO", "Midwest"},
            {"Wisconsin", "Midwest"}, {"WI", "Midwest"},
            {"Illinois", "Midwest"}, {"IL", "Midwest"},
            {"Indiana", "Midwest"}, {"IN", "Midwest"},
            {"Ohio", "Midwest"}, {"OH", "Midwest"},
            {"Michigan", "Midwest"}, {"MI", "Midwest"},
            {"Oklahoma", "SouthWest"}, {"OK", "SouthWest"},
            {"Texas", "SouthWest"}, {"TX", "SouthWest"},
            {"New Mexico", "SouthWest"}, {"NM", "SouthWest"},
            {"Arizona", "SouthWest"}, {"AZ", "SouthWest"},
            {"Colorado", "West"}, {"CO",  "West"},
            {"Wyoming",  "West"}, {"WY",  "West"},
            {"Montana", "West"}, {"MT", "West"},
            {"Idaho", "West"}, {"ID", "West"},
            {"Utah", "West"}, {"UT", "West"},
            {"Nevada", "West"}, {"NV", "West"},
            {"Oregon", "West"}, {"OR", "West"},
            {"Washington", "West"}, {"WA", "West"},
            {"California", "West"}, {"CA", "West"},
            {"Hawaii", "West"}, {"HI", "West"},
            {"Alaska", "West"}, {"AK", "West"}
        };
        */

        public static double ShippingEstimator(string state)
        {
            Dictionary<String, String> states = new Dictionary<String, String>();

            // Manually Add
            states.Add("Maine", "NorthEast");
            states.Add("ME", "NorthEast");
            states.Add("Vermont", "NorthEast");
            states.Add("VT", "NorthEast");
            states.Add("New Hampshire", "NorthEast");
            states.Add("NH", "NorthEast");
            states.Add("Massachusetts", "NorthEast");
            states.Add("MA", "NorthEast");
            states.Add("Connecticut", "NorthEast");
            states.Add("CT", "NorthEast");
            states.Add("Rhode Island", "NorthEast");
            states.Add("RI", "NorthEast");
            states.Add("New York", "NorthEast");
            states.Add("NY", "NorthEast");
            states.Add("New Jersey", "NorthEast");
            states.Add("NJ", "NorthEast");
            states.Add("Pennsylvania", "NorthEast");
            states.Add("PA", "NorthEast");
            states.Add("Delaware", "NorthEast");
            states.Add("DE", "NorthEast");
            states.Add("Maryland", "NorthEast");
            states.Add("MD", "NorthEast");
            states.Add("Washington D.C.", "SouthEast");
            states.Add("DC", "SouthEast");
            states.Add("West Virginia", "SouthEast");
            states.Add("WV", "SouthEast");
            states.Add("Virginia", "SouthEast");
            states.Add("VA", "SouthEast");
            states.Add("North Carolina", "SouthEast");
            states.Add("NC", "SouthEast");
            states.Add("South Carolina", "SouthEast");
            states.Add("SC", "SouthEast");
            states.Add("Georgia", "SouthEast");
            states.Add("GA", "SouthEast");
            states.Add("Florida", "SouthEast");
            states.Add("FL", "SouthEast");
            states.Add("Alabama", "SouthEast");
            states.Add("AL", "SouthEast");
            states.Add("Tennessee", "SouthEast");
            states.Add("TN", "SouthEast");
            states.Add("Kentucky", "SouthEast");
            states.Add("KY", "SouthEast");
            states.Add("Arkansas", "SouthEast");
            states.Add("AR", "SouthEast");
            states.Add("Louisiana", "SouthEast");
            states.Add("LA", "SouthEast");
            states.Add("North Dakota", "Midwest");
            states.Add("ND", "Midwest");
            states.Add("South Dakota", "Midwest");
            states.Add("SD", "Midwest");
            states.Add("Nebraska", "Midwest");
            states.Add("NE", "Midwest");
            states.Add("Kansas", "Midwest");
            states.Add("KS", "Midwest");
            states.Add("Minnesota", "Midwest");
            states.Add("MN", "Midwest");
            states.Add("Iowa", "Midwest");
            states.Add("IA", "Midwest");
            states.Add("Missouri", "Midwest");
            states.Add("MO", "Midwest");
            states.Add("Wisconsin", "Midwest");
            states.Add("WI", "Midwest");
            states.Add("Illinois", "Midwest");
            states.Add("IL", "Midwest");
            states.Add("Indiana", "Midwest");
            states.Add("IN", "Midwest");
            states.Add("Ohio", "Midwest");
            states.Add("OH", "Midwest");
            states.Add("Michigan", "Midwest");
            states.Add("MI", "Midwest");
            states.Add("Oklahoma", "SouthWest");
            states.Add("OK", "SouthWest");
            states.Add("Texas", "SouthWest");
            states.Add("TX", "SouthWest");
            states.Add("New Mexico", "SouthWest");
            states.Add("NM", "SouthWest");
            states.Add("Arizona", "SouthWest");
            states.Add("AZ", "SouthWest");
            states.Add("Colorado", "West");
            states.Add("CO", "West");
            states.Add("Wyoming", "West");
            states.Add("WY", "West");
            states.Add("Montana", "West");
            states.Add("MT", "West");
            states.Add("Idaho", "West");
            states.Add("ID", "West");
            states.Add("Utah", "West");
            states.Add("UT", "West");
            states.Add("Nevada", "West");
            states.Add("NV", "West");
            states.Add("Oregon", "West");
            states.Add("OR", "West");
            states.Add("Washington", "West");
            states.Add("WA", "West");
            states.Add("California", "West");
            states.Add("CA", "West");
            states.Add("Hawaii", "West");
            states.Add("HI", "West");
            states.Add("Alaska", "West");
            states.Add("AK", "West");
       
            double rate = 0;

            if (states.ContainsKey(state))
            {
                string region = states[state];

                if (region == "West")
                {
                    rate = 4.99;
                }
                else if (region == "SouthWest")
                {
                    rate = 6.99;
                }
                else if (region == "Midwest")
                {
                    rate = 8.99;
                }
                else if (region == "SouthEast")
                {
                    rate = 9.99;
                }
                else if (region == "NorthEast")
                {
                    rate = 10.99;
                }
                else
                {
                    rate = 12.99;
                }
            }
            else
            {
                rate = 12.99;
            }

            return rate;
        }

        public static double Volume(double length, double width, double height)
        {
            return length * width * height;
        }
    }
}
