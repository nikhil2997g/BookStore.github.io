using BookStore.Data;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class BookRepository
    {
        private BookStoreContext _context = null;

        public BookRepository(BookStoreContext context)
        {
            _context = context;
        }

        public async Task<int> AddNewBook(BookModel model)
        {
            var newbook = new Books()
            {
                Author = model.Author,
                CreatedOn = DateTime.UtcNow,
                Description = model.Description,
                Title = model.Title,
                LanguageId = (int)model.LanguageId,
                TotalPages = model.TotalPages.HasValue ? model.TotalPages.Value : 0,
                UpdatedOn = DateTime.UtcNow
            };

           await _context.books.AddAsync(newbook);
           await _context.SaveChangesAsync();

            return newbook.Id;
        }
        public async Task<List<BookModel>> GetAllBooks()
        {
            var books = new List<BookModel>();
            var allbooks = await _context.books.ToListAsync();
            if (allbooks?.Any() == true)
            {
                foreach (var book in allbooks)
                {
                    books.Add(new BookModel()
                    {
                        Author = book.Author,
                        Category = book.Category,
                        Description = book.Description,
                        Id = book.Id,
                        LanguageId = book.LanguageId,
                        Language = book.Language.Name,
                        Title = book.Title,
                        TotalPages = book.TotalPages
                    });
                }
            }
            return books;
        }

        public async Task<BookModel> GetBookbyId(int id)
        {
            return await _context.books.Where(b => b.Id == id).Select(book => new BookModel()
            {
                Author = book.Author,
                Category = book.Category,
                Description = book.Description,
                Id = book.Id,
                LanguageId = book.LanguageId,
                Language = book.Language.Name,
                Title = book.Title,
                TotalPages = book.TotalPages
            }).FirstOrDefaultAsync();

        }

        public List<BookModel> SearchBooks(string title, string author)
        {
            //return DataSource().Where(s => s.Title == title && s.Author == author).ToList();
            return null;
        }
    }
}
