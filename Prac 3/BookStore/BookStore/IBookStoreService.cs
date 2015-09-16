using System;
using System.Collections.Generic;
using System.Data;
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
        //List<Book> searchBook(string ID, string name, string Author, int year);
        List<Book> searchBook(int year);
    }

    [DataContract]
    public class Book
    {
        public string ID, name, author;
        public int num, year, stock;
        public float price;

        [DataMember]
        public int BookNum
        {
            get;
            set;
        }

        [DataMember]
        public string BookID
        {
            get;
            set;
        }

        [DataMember]
        public string BookName
        {
            get;
            set;
        }

        [DataMember]
        public string BookAuthor
        {
            get;
            set;
        }

        [DataMember]
        public int BookYear
        {
            get;
            set;
        }

        [DataMember]
        public int BookStock
        {
            get;
            set;
        }

        [DataMember]
        public float BookPrice
        {
            get;
            set;
        }
    }
}
