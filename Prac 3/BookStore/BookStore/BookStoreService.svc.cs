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
        public void DoWork()
        {

        }

        //public Book GetBook(string ID)
        //{
        //    Book BookInfo = new Book();

        //    //set directory as desktop for STOCK and STORE
        //    string path = HttpRuntime.AppDomainAppPath;
        //    string file = @"Books.txt";
        //    string fullFilename = path + file;

        //    int count = 0;
        //    string line;
        //    bool BookExist = false;

        //    if (File.Exists(fullFilename))
        //    {
        //      StreamReader fileReader = new StreamReader(fullFilename);
        //        while ((line = fileReader.ReadLine()) != null)
        //        {
        //            string[] delimiters = {
        //                                      ",",
        //                                      "\r\n",
        //                                      ":"
        //                                  };
        //            string[] arrayBooks = line.Split(delimiters, 
        //                StringSplitOptions.RemoveEmptyEntries)
        //                .Select(a => a.Trim())
        //                .ToArray();

        //            if (arrayBooks[1] == ID)
        //            {
        //                BookInfo.ID = split[1];
        //                BookInfo.name = split[3];
        //                BookInfo.branchNO = Convert.ToInt32(split[5]);
        //                BookInfo.address = split[7];
        //                BookInfo.phoneNumber = Convert.ToInt32(split[9]);
        //                BookExist = true;
        //            }
        //            count++;
        //        }
        //        fileReader.Close();

        //    }
        //    if (BookExist)
        //    {
        //        return BookInfo;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        public List<Book> GetAllBooks()
        {
            List<Book> BookInfo = new List<Book>();
            Book newBookInfo = new Book();

            string path = HttpRuntime.AppDomainAppPath;
            string file = @"Books.txt";
            string fullPathname = path + file;

            string[] readerBooks = ReadLines().ToArray();
            Debug.WriteLine(readerBooks);

            //if (File.Exists(fullPathname))
            //{
            //    StreamReader fileReader = new StreamReader(fullPathname);

            //    string line;
            //    while ((line = fileReader.ReadLine()) != null)
            //    {
            string[] delimiters = {
                                              ",",
                                              "\r\n",
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
                    BookNum = (i + 1).ToString(),
                    BookID = arrayBooks[0],
                    BookName = arrayBooks[1],
                    BookAuthor = arrayBooks[2],
                    BookYear = int.Parse(arrayBooks[3]),
                    BookPrice = '$' + float.Parse(arrayBooks[4].Trim('$')),
                    BookStock = int.Parse(arrayBooks[5])
                });
            }

            foreach (Book book in BookInfo)
            {
                return BookInfo;
                Debug.WriteLine(BookInfo);
            }
            //}
            //fileReader.Close();
            //}

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
            throw new NotImplementedException();
        }

        public IEnumerable<String> ReadLines()
        {
            string file = "Books.txt";
            string sourcePath = HttpRuntime.AppDomainAppPath;
            Debug.WriteLine(HttpRuntime.AppDomainAppPath);

            StreamReader readerSuburbPostcode = new StreamReader(sourcePath + file);

            string line;
            while ((line = readerSuburbPostcode.ReadLine()) != null)
            {
                yield return line;
                Debug.WriteLine(line);
            }
        }
    }
}
