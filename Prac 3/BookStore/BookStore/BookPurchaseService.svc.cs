using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web;

namespace BookStore
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "BookPurchaseService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select BookPurchaseService.svc or BookPurchaseService.svc.cs at the Solution Explorer and start debugging.
    public class BookPurchaseService : IBookPurchaseService
    {
        private static string path = HttpRuntime.AppDomainAppPath;
        private static string file = @"books.txt";
        private string finalPathname = path + file;

        BookPurchaseResponse thisResponse = new BookPurchaseResponse();

        public BookPurchaseResponse PurchaseBooks(BookPurchaseInfo purchaseInfo)
        {
            float totalCost = 0;
            float thisBudget = purchaseInfo.budget;

            Dictionary<int, int> items = purchaseInfo.items;

            foreach (KeyValuePair<int, int> item in items)
            {
                int thisID = item.Key;
                int thisQty = item.Value;

                float getCost = getBookCost(thisID);
                int getQty = getBookQty(thisID);

                if (getQty < thisQty)
                {
                    thisResponse.result = false;
                    thisResponse.response = "Not enough stocks available. Stock available: " + getQty;

                    return thisResponse;
                }

                totalCost += (getCost * thisQty);
            }

            if (totalCost < thisBudget)
            {
                thisResponse.result = true;
                try
                {
                    float balance = thisBudget - totalCost;
                    thisResponse.response = String.Format("{0:0.##}", balance);
                }
                catch (Exception Ex)
                {
                    throw new FaultException<Exception>(new Exception(Ex.Message));
                }
            }
            else
            {
                thisResponse.result = false;
                thisResponse.response = "Not enough money. You are $" + (totalCost - thisBudget) + " short.";
            }
            return thisResponse;
        }

        private float getBookCost(int bookNum)
        {
            float bookCost = 0;

            try
            {
                string line = File.ReadLines(finalPathname).Skip(bookNum - 1).Take(1).First(); //access the particular book
                String[] bookDetails = line.Split(','); //split the line into an array entry
                bookCost = float.Parse(bookDetails[4].Trim('$')); //access the cost of the book
            }
            catch (Exception Ex)
            {
                throw new FaultException<Exception>(new Exception(Ex.Message));
            }
            return bookCost;
        }

        private int getBookQty(int bookNum)
        {
            int bookQty = 0;

            try
            {
                string line = File.ReadLines(finalPathname).Skip(bookNum - 1).Take(1).First(); //access the particular book
                String[] bookDetails = line.Split(','); //split the line into an array entry
                bookQty = int.Parse(bookDetails[5]); //access the qty of the book available
            }
            catch (Exception Ex)
            {
                throw new FaultException<Exception>(new Exception(Ex.Message));
            }
            return bookQty;
        }

        private BookPurchaseInfo BookPurchaseInfo(string total_budget, Dictionary<string, string> purchaseBook)
        {
            BookPurchaseInfo thisPurchaseInfo = new BookPurchaseInfo();
            thisPurchaseInfo.budget = float.Parse(total_budget);
            thisPurchaseInfo.items = purchaseBook.ToDictionary(
                x => int.Parse(x.Key),
                x => int.Parse(x.Value)
                );

            return thisPurchaseInfo;
        }

    }
}