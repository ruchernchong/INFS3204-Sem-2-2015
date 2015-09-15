using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BookStore
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "BookStoreService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select BookStoreService.svc or BookStoreService.svc.cs at the Solution Explorer and start debugging.
    public class BookStoreService : IBookStoreService
    {
        public void DoWork()
        {
        }

        public List<Book> GetAllBooks()
        {
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
    }
}
