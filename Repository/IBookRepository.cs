using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public interface IBookRepository
    {
        BookModel GetBookbyId(int id);
        IEnumerable<BookModel> GetAllBooks();
        BookModel AddBook(BookModel bookModel);
        BookModel Update(BookModel bookchanges);
        BookModel Delete(int id);
    }
}
