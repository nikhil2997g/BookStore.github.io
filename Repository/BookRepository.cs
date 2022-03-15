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
                new BookModel(){Id = 1, Title = "MVC", Author = "Nithish", Description = "This is Description for MVC Book", Category = "Framework", Language = "English", TotalPages = 897 },
                new BookModel(){Id = 2, Title = "Dot net core", Author = "Nithish", Description = "This is Description for dot net core Book", Category = "Development", Language = "English", TotalPages = 597 },
                new BookModel(){Id = 3, Title = "Java", Author = "authorj", Description = "This is Description for java Book", Category = "Programming", Language = "English", TotalPages = 397 }
            };
        }
    }
}
