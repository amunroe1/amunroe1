using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

/**************************************************
 *      CSE 598 - Assignment 3 Ex. 2.2 
 *              - An Implementation for a WSDL service
 *                acting as a wrapper for eBay's 
 *                Search API's
 *         
 *       author - Mark Adan
 *      version - 1.0
 * Last updated - 10/3/25
 **************************************************/

namespace MyEbayService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IMyEbayService1" in both code and config file together.
    [ServiceContract]
    public interface IMyEbayService1
    {
        [OperationContract]
        Task<string> SearchItem(string queryItem);

        [OperationContract]
        Task<string> GetItem(string listingId);

    }

}
