using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace BookStore
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "BookStoreService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select BookStoreService.svc or BookStoreService.svc.cs at the Solution Explorer and start debugging.
    public class BookStoreService : IBookStoreService
    {
        private static string path = HttpRuntime.AppDomainAppPath;
        private static string file = @"books.txt";
        private string finalPathname = path + file;

        private string tempFile = Path.GetTempFileName();

        public Book createBook(string[] bookDetails, int num)
        {
            List<string> fieldError = new List<string>();
            Boolean isFieldEmpty = false;
            for (int i = 0; i <= 5; i++)
            {
                if (String.IsNullOrWhiteSpace(bookDetails[i]))
                {
                    isFieldEmpty = true;

                    switch (i)
                    {
                        case 0:
                            fieldError.Add("ID");
                            break;
                        case 1:
                            fieldError.Add("Name");
                            break;
                        case 2:
                            fieldError.Add("Author");
                            break;
                        case 3:
                            fieldError.Add("Year");
                            break;
                        case 4:
                            fieldError.Add("Price");
                            break;
                        case 5:
                            fieldError.Add("Stock");
                            break;
                        default:
                            break;
                    }
                }
            }

            if (isFieldEmpty)
            {
                string concatFieldErrors = String.Join(", ", fieldError.ToArray());

                throw new ArgumentException("The following field(s) are empty: ", concatFieldErrors);
            }

            bool isPositiveYear = int.Parse(bookDetails[3]) > 0;
            bool isPositivePrice = float.Parse(bookDetails[4].Trim('$')) > 0;
            bool isPositiveStock = int.Parse(bookDetails[5]) > 0;

            if (!isPositiveYear || !isPositivePrice || !isPositiveStock)
            {
                throw new ArgumentException("Input for Year, Price and Stock must be positive.");
            }

            Book thisBook = new Book();

            thisBook.num = num;
            thisBook.ID = bookDetails[0];
            thisBook.name = bookDetails[1];
            thisBook.author = bookDetails[2];
            thisBook.year = int.Parse(bookDetails[3]);
            thisBook.price = float.Parse(bookDetails[4].Trim('$'));
            thisBook.stock = int.Parse(bookDetails[5]);

            return thisBook;
        }

        public ICollection GetAllBooks()
        {
            DataTable dataTable = new DataTable();
            DataRow dataRow;
            DataColumn dataColumnIndex = new DataColumn();

            dataColumnIndex.DataType = typeof(String);
            dataColumnIndex.AutoIncrement = true;
            dataColumnIndex.AutoIncrementSeed = 1;
            dataColumnIndex.AutoIncrementStep = 1;
            dataColumnIndex.ColumnName = "Num";

            dataTable.Columns.Add(dataColumnIndex);
            //dataTable.Columns.Add(new DataColumn("Num", typeof(String)));
            dataTable.Columns.Add(new DataColumn("ID", typeof(String)));
            dataTable.Columns.Add(new DataColumn("Name", typeof(String)));
            dataTable.Columns.Add(new DataColumn("Author", typeof(String)));
            dataTable.Columns.Add(new DataColumn("Year", typeof(int)));
            dataTable.Columns.Add(new DataColumn("Price ($)", typeof(String)));
            dataTable.Columns.Add(new DataColumn("Stock", typeof(int)));

            String[] readerBooks = ReadLines().ToArray();
            Debug.WriteLine(readerBooks);
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

                Debug.WriteLine(arrayBooks);

                dataRow = dataTable.NewRow();

                dataRow[1] = arrayBooks[0]; // ID of Book
                dataRow[2] = arrayBooks[1]; // Name of Book
                dataRow[3] = arrayBooks[2]; // Author of Book
                dataRow[4] = arrayBooks[3]; // Year of Publication of Book
                dataRow[5] = arrayBooks[4]; // Price of Book
                dataRow[6] = arrayBooks[5]; // Stock of Book

                dataTable.Rows.Add(dataRow);
            }

            DataView dataView = new DataView(dataTable);
            return dataView;

            throw new NotImplementedException();
        }

        public bool addBook(String[] newBook)
        {
            if (File.Exists(finalPathname))
            {
                try
                {
                    using (StreamWriter writerBooks = new StreamWriter(finalPathname, true))
                    {
                        writerBooks.WriteLineAsync(String.Format(
                            "{0},{1},{2},{3},${4},{5}",
                            newBook[0],
                            newBook[1],
                            newBook[2],
                            newBook[3],
                            newBook[4],
                            newBook[5]
                            ));

                        return true;
                    }
                }
                catch (Exception Ex)
                {
                    throw new FaultException<Exception>(new Exception(Ex.Message));
                }
            }
            else
            {
                return false;
            }

            throw new NotImplementedException();
        }

        public bool deleteBook(string type, string input)
        public Boolean updateRecord(Book book, int updatedStock)
        {
            Book thisBook = book;

            String[] bookDetails = {
                                       thisBook.ID,
                                       thisBook.name,
                                       thisBook.author,
                                       thisBook.year.ToString(),
                                       thisBook.price.ToString(),
                                       updatedStock.ToString()
                                   };
            Book newBook = createBook(bookDetails, 0);

            string thisID = thisBook.ID;
            string type = "ID";
            deleteBook(type, thisID);
            AddNewRecord(newBook);

            return true;
        }
            try
            {
                using (StreamReader readerBooks = new StreamReader(finalPathname))
                {
                    string line;
                    bool isDeleted = false;
                    List<String> bookLines = new List<String>();
                    String[] delimiters = {
                                         ",",
                                         "\r\n"
                                     };

                    while ((line = readerBooks.ReadLine()) != null)
                    {
                        String[] arrayBooks = line.Split(delimiters,
                        StringSplitOptions.RemoveEmptyEntries);

                        switch (type)
                        {
                            case "ID":
                                if (arrayBooks[0].Equals(input))
                                {
                                    isDeleted = true;
                                }
                                else
                                {
                                    bookLines.Add(line);
                                }
                                break;

                            case "Name":
                                if (arrayBooks[1].Equals(input))
                                {
                                    isDeleted = true;
                                }
                                else
                                {
                                    bookLines.Add(line);
                                }
                                break;

                            case "Year":
                                try
                                {
                                    if (int.Parse(arrayBooks[3]) == int.Parse(input))
                                    {
                                        isDeleted = true;
                                    }
                                    else
                                    {
                                        bookLines.Add(line);
                                    }
                                }
                                catch
                                {
                                    throw new FaultException<Exception>(new Exception(input));
                                }
                                break;
                            default:
                                return false;
                        }
                    }
                    readerBooks.Close();

                    if (isDeleted)
                    {
                        try
                        {
                            using (StreamWriter writerBooks = new StreamWriter(tempFile))
                            {
                                foreach (string bookLine in bookLines)
                                {
                                    writerBooks.WriteLine(bookLine);
                                }
                            }
                        }
                        catch (Exception Ex)
                        {
                            throw new FaultException<Exception>(new Exception(Ex.Message));
                        }

                        deleteFile(); //Call deleteFile() method;

                        return true;
                    }
                    return false;
                }
            }
            catch (Exception Ex)
            {
                throw new FaultException<Exception>(new Exception(Ex.Message));
            }
        }

        private void deleteFile()
        {
            try
            {
                if (File.Exists(finalPathname))
                {
                    File.Delete(finalPathname);
                    File.Move(tempFile, finalPathname);
                }
            }
            catch (Exception Ex)
            {
                throw new FaultException<Exception>(new Exception(Ex.Message));
            }
        }

        public ICollection searchBook(string type, string input)
        {
            DataTable dataTable = new DataTable();
            DataRow dataRow;
            DataColumn dataColumnIndex = new DataColumn();

            dataColumnIndex.DataType = typeof(String);
            dataColumnIndex.AutoIncrement = true;
            dataColumnIndex.AutoIncrementSeed = 1;
            dataColumnIndex.AutoIncrementStep = 1;
            dataColumnIndex.ColumnName = "Num";

            dataTable.Columns.Add(dataColumnIndex);
            dataTable.Columns.Add(new DataColumn("ID", typeof(String)));
            dataTable.Columns.Add(new DataColumn("Name", typeof(String)));
            dataTable.Columns.Add(new DataColumn("Author", typeof(String)));
            dataTable.Columns.Add(new DataColumn("Year", typeof(int)));
            dataTable.Columns.Add(new DataColumn("Price ($)", typeof(String)));
            dataTable.Columns.Add(new DataColumn("Stock", typeof(int)));

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

                switch (type)
                {
                    case "ID":
                        try
                        {
                            if (arrayBooks[0].Equals(input))
                            {
                                dataRow = dataTable.NewRow();

                                    dataRow[1] = arrayBooks[0]; // ID of Book
                                    dataRow[2] = arrayBooks[1]; // Name of Book
                                    dataRow[3] = arrayBooks[2]; // Author of Book
                                    dataRow[4] = arrayBooks[3]; // Year of Publication of Book
                                    dataRow[5] = arrayBooks[4]; // Price of Book
                                    dataRow[6] = arrayBooks[5]; // Stock of Book

                                dataTable.Rows.Add(dataRow);
                            }
                        }
                        catch (Exception Ex)
                        {
                            throw new FaultException<Exception>(new Exception(Ex.Message));
                        }
                        break;
                    case "Name":
                        try
                        {
                            if (arrayBooks[1].ToLower().Contains(input.ToLower()))
                            {
                                dataRow = dataTable.NewRow();

                                    dataRow[1] = arrayBooks[0]; // ID of Book
                                    dataRow[2] = arrayBooks[1]; // Name of Book
                                    dataRow[3] = arrayBooks[2]; // Author of Book
                                    dataRow[4] = arrayBooks[3]; // Year of Publication of Book
                                    dataRow[5] = arrayBooks[4]; // Price of Book
                                    dataRow[6] = arrayBooks[5]; // Stock of Book

                                dataTable.Rows.Add(dataRow);
                            }
                        }
                        catch (Exception Ex)
                        {
                            throw new FaultException<Exception>(new Exception(Ex.Message));
                        }
                        break;
                    case "Author":
                        try
                        {
                            if (arrayBooks[2].ToLower().Contains(input.ToLower()))
                            {
                                dataRow = dataTable.NewRow();

                                    dataRow[1] = arrayBooks[0]; // ID of Book
                                    dataRow[2] = arrayBooks[1]; // Name of Book
                                    dataRow[3] = arrayBooks[2]; // Author of Book
                                    dataRow[4] = arrayBooks[3]; // Year of Publication of Book
                                    dataRow[5] = arrayBooks[4]; // Price of Book
                                    dataRow[6] = arrayBooks[5]; // Stock of Book

                                dataTable.Rows.Add(dataRow);
                            }
                        }
                        catch (Exception Ex)
                        {
                            throw new FaultException<Exception>(new Exception(Ex.Message));
                        }
                        break;
                    case "Year":
                        try
                        {
                            if (int.Parse(arrayBooks[3]).Equals(int.Parse(input)))
                            {
                                dataRow = dataTable.NewRow();

                                    dataRow[1] = arrayBooks[0]; // ID of Book
                                    dataRow[2] = arrayBooks[1]; // Name of Book
                                    dataRow[3] = arrayBooks[2]; // Author of Book
                                    dataRow[4] = arrayBooks[3]; // Year of Publication of Book
                                    dataRow[5] = arrayBooks[4]; // Price of Book
                                    dataRow[6] = arrayBooks[5]; // Stock of Book

                                dataTable.Rows.Add(dataRow);
                            }
                        }
                        catch (Exception Ex)
                        {
                            throw new FaultException<Exception>(new Exception(Ex.Message));
                        }
                        break;
                    default:
                        break;
                }
            }

            DataView dataView = new DataView(dataTable);
            return dataView;

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
    }
}
