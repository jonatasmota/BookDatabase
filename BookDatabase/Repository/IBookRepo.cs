using BookDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookDatabase.Repository
{
    public interface IBookRepo
    {
        BookModel FindById(int id);
        List<BookModel> FindAll();
        BookModel Add(BookModel book);
        BookModel Update(BookModel book);
        bool Delete(int id);
    }
}
