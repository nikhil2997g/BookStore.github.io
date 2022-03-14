using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class BookRepository
    {
        public List<BookModel> GetAllBooks()
        {
            return DataSource();
        }

        public BookModel GetBookbyId(int id)
        {
            return DataSource().Where(b => b.Id == id).FirstOrDefault();
        }

        public List<BookModel> SearchBooks(string title, string author)
        {
            return DataSource().Where(s => s.Title == title && s.Author == author).ToList();
        }

        private List<BookModel> DataSource()
        {
            return new List<BookModel>()
            {
                new BookModel(){Id = 1, Title = "MVC", Author = "Nithish" },
                new BookModel(){Id = 2, Title = "dot net core", Author = "Nithish" },
                new BookModel(){Id = 3, Title = "java", Author = "authorj" }
            };
        }
    }
}
