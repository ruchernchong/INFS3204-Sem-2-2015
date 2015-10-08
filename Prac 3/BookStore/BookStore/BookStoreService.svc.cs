using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
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

        public Book createBook(String[] bookDetails, int num)
        {
            List<String> fieldError = new List<String>();
            bool isFieldEmpty = false;
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
                throw new ArgumentException("Input(s) for Year, Price and Stock must be positive.");
            }

            if (!IsYear(bookDetails[3]))
            {
                throw new FormatException("Year must be valid and contain exactly 4 chars.");
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


                for (int j = 0; j < arrayBooks.GetLength(0); j++)
                {
                    dataRow[j + 1] = arrayBooks[j]; // j+1 because index is pre-defined.
                }

                dataTable.Rows.Add(dataRow);
            }

            DataView dataView = new DataView(dataTable);
            return dataView;
        }

        public Boolean addBook(String[] newBook)
        {
            Book thisBook = createBook(newBook, 0);
            bool isUpdatedBookStock = true;

            try
            {
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

                    if (arrayBooks[0] == thisBook.ID)
                    {
                        int newStock = thisBook.stock;
                        int oldStock = int.Parse(arrayBooks[5]);
                        int updatedStock = oldStock + newStock;

                        updateRecord(thisBook, updatedStock);
                        isUpdatedBookStock = true;

                        return true;
                    }
                }
            }
            catch (Exception Ex)
            {
                throw new FaultException<Exception>(new Exception(Ex.Message));
            }

            if (isUpdatedBookStock)
            {
                AddNewRecord(thisBook);
                return true;
            }
            return false;
        }

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

        public Boolean AddNewRecord(Book book)
        {
            Book thisBook = book;

            try
            {
                using (StreamWriter writerBooks = new StreamWriter(finalPathname, true))
                {
                    writerBooks.WriteLineAsync(String.Format(
                        "{0},{1},{2},{3},${4:0.00},{5}",
                        thisBook.ID,
                        thisBook.name,
                        thisBook.author,
                        thisBook.year,
                        thisBook.price,
                        thisBook.stock
                        ));

                    writerBooks.Close();

                    return true;
                }
            }
            catch (Exception Ex)
            {
                return false;
                throw new FaultException<Exception>(new Exception(Ex.Message));
            }
        }

        public Boolean deleteBook(string type, string input)
        {
            if (String.IsNullOrWhiteSpace(input))
            {
                throw new NullReferenceException("Input is empty.");
            }
            else if (type.Equals("Year") && !IsYear(input))
            {
                throw new FormatException("Input is not a valid year.");
            }
            else if (type.Equals("Year") && !IsPositive(int.Parse(input)))
            {
                throw new FormatException("Year must be a valid positive integer. Input: " + input);
            }
            else
            {
                try
                {
                    using (StreamReader readerBooks = new StreamReader(finalPathname))
                    {
                        string line;
                        bool isDeleted = false;
                        int columnIndex = 1;
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

                                case "Num":
                                    try
                                    {
                                        if (columnIndex == int.Parse(input))
                                        {
                                            isDeleted = true;
                                        }
                                        else
                                        {
                                            bookLines.Add(line);
                                        }
                                        break;
                                    }
                                    catch (FormatException thisFormatException)
                                    {
                                        throw new FormatException(thisFormatException.Message);
                                    }
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
                                    catch (Exception Ex)
                                    {
                                        throw new Exception(Ex.Message);
                                    }
                                    break;
                                default:
                                    return false;
                            }
                            columnIndex++;
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
                catch (FormatException thisFormatException)
                {
                    throw new FormatException(thisFormatException.Message);
                }
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

                    if (String.IsNullOrWhiteSpace(input))
                    {
                        throw new NullReferenceException("Input is empty.");
                    }
                    else if (type.Equals("Year") && !IsPositive(int.Parse(input)))
                    {
                        throw new FormatException("Year must be a valid positive integer. Input: " + input);
                    }
                    else
                else if (type.Equals("Year") && !IsYear(input))
                {
                    throw new ArgumentException("Input is not a valid year.");
                }
                    {
                        switch (type)
                        {
                            case "ID":
                                try
                                {
                                    if (arrayBooks[0].Equals(input))
                                    {
                                        dataRow = dataTable.NewRow();

                                        for (int j = 0; j < arrayBooks.GetLength(0); j++)
                                        {
                                            dataRow[j + 1] = arrayBooks[j]; // j+1 because index is pre-defined.
                                        }

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

                                        for (int j = 0; j < arrayBooks.GetLength(0); j++)
                                        {
                                            dataRow[j + 1] = arrayBooks[j]; // j+1 because index is pre-defined.
                                        }

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

                                        for (int j = 0; j < arrayBooks.GetLength(0); j++)
                                        {
                                            dataRow[j + 1] = arrayBooks[j]; // j+1 because index is pre-defined.
                                        }

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

                                        for (int j = 0; j < arrayBooks.GetLength(0); j++)
                                        {
                                            dataRow[j + 1] = arrayBooks[j]; // j+1 because index is pre-defined.
                                        }

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
            }

            DataView dataView = new DataView(dataTable);
            return dataView;
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

        private Boolean IsPositive(int number)
        {
            return number > 0;
        }

        private Boolean IsYear(string year)
        {
            string isYear = @"^(19|20)[0-9][0-9]";

            if (Regex.IsMatch(year, isYear))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}