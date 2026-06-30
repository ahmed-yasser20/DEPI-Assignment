using BookStore.Entity;
using BookStore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public class BookService : IBookService
    {
        private readonly IRepository<Book> _repository;

        public BookService(IRepository<Book> repository)
        {
            _repository = repository;
        }

        public void AddBook(Book book)
        {
            if (string.IsNullOrWhiteSpace(book.Title))
                throw new Exception("Book title is required.");

            if (book.Price <= 0)
                throw new Exception("Price must be greater than zero.");

            if (book.Stock < 0)
                throw new Exception("Stock cannot be negative.");

            _repository.Add(book);
        }

        public List<Book> GetAllBooks()
        {
            return _repository.GetAll();
        }

        public Book? GetBookById(int id)
        {
            return _repository.GetById(id);
        }

        public void RemoveBook(int id)
        {
            var book = _repository.GetById(id);

            if (book == null)
                throw new Exception("Book not found.");

            _repository.Remove(id);
        }

        public Book? SearchBook(int id)
        {
            return _repository.GetById(id);
        }
    }

}
