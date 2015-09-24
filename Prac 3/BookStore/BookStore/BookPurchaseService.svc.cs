using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BookStore
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "BookPurchaseService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select BookPurchaseService.svc or BookPurchaseService.svc.cs at the Solution Explorer and start debugging.
    public class BookPurchaseService : IBookPurchaseService
    {
        private static string path = HttpRuntime.AppDomainAppPath;
        private static string file = @"books.txt";
        private string finalPathname = path + file;

        //private string tempFile = Path.GetTempFileName();

        public BookPurchaseInfo PurchaseBook(BookPurchaseResponse Response)
        {
            throw new NotImplementedException();
        }

        public Dictionary<int, int> Books(int input)
        {
            Dictionary<int, int> Books = new Dictionary<int, int>();

            String[] readerBooks = ReadLines().ToArray();
            String[] delimiters = {
                                      ",",
                                      "\r\n"
                                  };

            for (int i = 0; i < readerBooks.GetLength(0); i++)
            {
                String[] arrayBooks = readerBooks[i].Split(delimiters,
                    StringSplitOptions.RemoveEmptyEntries)
                    .Select(Book => Book.Trim())
                    .ToArray();

                Books.Add((i + 1), input);
            }

            return Books;

            throw new NotImplementedException();
        }

        public IEnumerable<String> ReadLines()
        {
            StreamReader readerBooks;
            string line;

            try
            {
                readerBooks = new StreamReader(finalPathname);
            }
            catch (Exception Ex)
            {
                throw new FaultException<Exception>(new Exception(Ex.Message));
            }

            while ((line = readerBooks.ReadLine()) != null)
            {
                yield return line;
                Debug.WriteLine(line);
            }
            readerBooks.Close();
        }

        public BookPurchaseInfo BookPurchaseInfo()
        {
            throw new NotImplementedException();
        }
    }
}
