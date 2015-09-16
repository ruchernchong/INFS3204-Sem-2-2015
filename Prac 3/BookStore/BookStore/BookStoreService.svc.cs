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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "BookStoreService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select BookStoreService.svc or BookStoreService.svc.cs at the Solution Explorer and start debugging.
    public class BookStoreService : IBookStoreService
    {
        public List<Book> BookInfo = new List<Book>();

        private static string path = HttpRuntime.AppDomainAppPath;
        private static string file = @"Books.txt";
        private string fullPathname = path + file;

        public List<Book> GetAllBooks()
        {
            string[] readerBooks = ReadLines().ToArray();
            Debug.WriteLine(readerBooks);

            string[] delimiters = {
                                      ",",
                                      "\r\n"
                                  };

            for (int i = 0; i < readerBooks.GetLength(0); i++)
            {
                string[] arrayBooks = readerBooks[i].Split(delimiters,
                    StringSplitOptions.RemoveEmptyEntries)
                    .Select(Book => Book.Trim())
                    .ToArray();

                Debug.WriteLine(arrayBooks);

                BookInfo.Add(new Book()
                {
                    BookNum = i + 1,
                    BookID = arrayBooks[0],
                    BookName = arrayBooks[1],
                    BookAuthor = arrayBooks[2],
                    BookYear = int.Parse(arrayBooks[3]),
                    BookPrice = '$' + float.Parse(arrayBooks[4].Trim('$')),
                    BookStock = int.Parse(arrayBooks[5])
                });
            }

            if (BookInfo.Count > 0) {
            foreach (Book book in BookInfo)
            {
                return BookInfo;
            }
        } else {
            return null;
        }
            throw new NotImplementedException();
        }

        public bool addBook(Book newBook)
        {
            throw new NotImplementedException();
        }

        public bool deleteBook(int year)
        {
            throw new NotImplementedException();
        }

        public List<Book> searchBook(int year)
        {
            string[] readerBooks = ReadLines().ToArray();

            string[] delimiters = {
                                      ",",
                                      "\r\n"
                                  };

            for (int i = 0; i < readerBooks.GetLength(0); i++)
            {
                string[] arrayBooks = readerBooks[i].Split(delimiters,
                    StringSplitOptions.RemoveEmptyEntries)
                    .Select(Book => Book.Trim())
                    .ToArray();

                if (year == int.Parse(arrayBooks[3]))
                {
                    Debug.WriteLine(int.Parse(arrayBooks[3]));

                    BookInfo.Add(new Book()
                    {
                        BookNum = i + 1,
                        BookID = arrayBooks[0],
                        BookName = arrayBooks[1],
                        BookAuthor = arrayBooks[2],
                        BookYear = int.Parse(arrayBooks[3]),
                        BookPrice = float.Parse(arrayBooks[4]),
                        BookStock = int.Parse(arrayBooks[5])
                    });
                }
            }

            foreach (Book book in BookInfo)
            {
                return BookInfo;
            }

            return null;

            throw new NotImplementedException();
        }

        public IEnumerable<String> ReadLines()
        {
            string file = "Books.txt";
            string sourcePath = HttpRuntime.AppDomainAppPath;
            Debug.WriteLine(HttpRuntime.AppDomainAppPath);

            StreamReader readerBooks = new StreamReader(sourcePath + file);

            string line;
            while ((line = readerBooks.ReadLine()) != null)
            {
                yield return line;
                Debug.WriteLine(line);
            }
            readerBooks.Close();
        }
    }
}
