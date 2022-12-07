using BookDatabase.Data;
using BookDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookDatabase.Repository
{
    public class BookRepo : IBookRepo
    {
        private readonly DatabaseContext _databaseContext;
        public BookRepo(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public BookModel FindById(int id)
        {
            return _databaseContext.Books.FirstOrDefault(x => x.Id == id);
        }

        public List<BookModel> FindAll()
        {
            return _databaseContext.Books.ToList();
        }

        public BookModel Add(BookModel book)
        {
            // Record to Database
            _databaseContext.Books.Add(book);
            _databaseContext.SaveChanges();

            return book;
        }

        public BookModel Update(BookModel book)
        {
            BookModel bookDb = FindById(book.Id);

            if (bookDb == null) throw new Exception("Error updating book.");

            bookDb.Title = book.Title;
            bookDb.Year = book.Year;
            bookDb.Author = book.Author;
            bookDb.Status = book.Status;
            bookDb.Image = book.Image;

            _databaseContext.Books.Update(bookDb);
            _databaseContext.SaveChanges();

            return bookDb;
        }

        public bool Delete(int id)
        {
            BookModel bookDb = FindById(id);

            if (bookDb == null) throw new Exception("Error deleting book.");

            _databaseContext.Books.Remove(bookDb);
            _databaseContext.SaveChanges();

            return true;
        }
    }
}
