using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BookStore
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IBookPurchaseService" in both code and config file together.
    [ServiceContract]
    public interface IBookPurchaseService
    {
        [OperationContract]
        [FaultContract(typeof(Exception))]
        BookPurchaseResponse PurchaseBooks(BookPurchaseInfo purchaseInfo);
    }

    [MessageContract]
    public class BookPurchaseInfo
    {
        [MessageHeader]
        public float budget;

        // Key is the (int) Index . Value is the (int) Quantity.
        [MessageHeader]
        public Dictionary<int, int> items;
    }

    [MessageContract]
    public class BookPurchaseResponse
    {
        [MessageHeader]
        public bool result;

        [MessageHeader]
        public string response;
    }
}