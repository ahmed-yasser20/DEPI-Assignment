using BookStore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public interface IBookService
    {
        void AddBook(Book book);

        void RemoveBook(int id);

        Book? SearchBook(int id);

        List<Book> GetAllBooks();
        Book? GetBookById(int id);
    }
}
