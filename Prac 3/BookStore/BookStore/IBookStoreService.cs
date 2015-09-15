using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BookStore
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWCFSvcBook" in both code and config file together.
    [ServiceContract]
    public interface IBookStoreService
    {
        [OperationContract]
        List<Book> GetAllBooks();

        [OperationContract]
        bool addBook(Book newBook);

        [OperationContract]
        bool deleteBook(int year);

        [OperationContract]
        List<Book> searchBook(int year);
    }

    [DataContract]
    public class Book
    {
        public string ID, name, author;
        public int year, stock;
        public float price;

        [DataMember]
        public string bookID
        {
            get { return ID; }
            set { ID = value; }
        }

        [DataMember]
        public string bookName
        {
            get { return name; }
            set { name = value; }
        }

        [DataMember]
        public string bookAuthor
        {
            get { return author; }
            set { author = value; }
        }

        [DataMember]
        public int bookYear
        {
            get { return year; }
            set { year = value; }
        }

        [DataMember]
        public int bookStock
        {
            get { return stock; }
            set { stock = value; }
        }

        [DataMember]
        public float bookPrice
        {
            get { return price; }
            set { price = value; }
        }
    }
}
