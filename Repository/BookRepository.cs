using BookStore.Data;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class BookRepository : IBookRepository
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
                Category = model.Category,
                Title = model.Title,
                LanguageId = (int)model.LanguageId,
                TotalPages = model.TotalPages.HasValue ? model.TotalPages.Value : 0,
                UpdatedOn = DateTime.UtcNow,
                CoverImageUrl = model.CoverImageUrl,
                BookPdfUrl = model.BookPdfUrl
            };

            newbook.bookGallery = new List<BookGallery>();
            foreach (var file in model.Gallery)
            {
                newbook.bookGallery.Add(new BookGallery()
                {
                    Name = file.Name,
                    URL = file.URL
                });
            }
            await _context.books.AddAsync(newbook);
            await _context.SaveChangesAsync();

            return newbook.Id;
        }
        public async Task<List<BookModel>> GetAllBooks()
        {

            return await _context.books.Select(book => new BookModel()
            {
                Author = book.Author,
                Category = book.Category,
                Description = book.Description,
                Id = book.Id,
                LanguageId = book.LanguageId,
                Language = book.Language.Name,
                Title = book.Title,
                TotalPages = book.TotalPages,
                CoverImageUrl = book.CoverImageUrl
            }).ToListAsync();
        }

        public async Task<BookModel> GetBookbyId(int id)
        {
            return await _context.books.Where(b => b.Id == id).Select(book => new BookModel()
            {
                Author = book.Author,
                Category = book.Category,
                Description = book.Description,
                Id = book.Id,
                CoverImageUrl = book.CoverImageUrl,
                LanguageId = book.LanguageId,
                Language = book.Language.Name,
                Title = book.Title,
                TotalPages = book.TotalPages,
                Gallery = book.bookGallery.Select(g => new GalleryModel()
                {
                    Id = g.Id,
                    Name = g.Name,
                    URL = g.URL
                }).ToList(),
                BookPdfUrl = book.BookPdfUrl
            }).FirstOrDefaultAsync();

        }

        public async Task<List<BookModel>> GetTopBooksAsync(int count)
        {
            return await _context.books.Select(book => new BookModel()
            {
                Author = book.Author,
                Category = book.Category,
                Description = book.Description,
                Id = book.Id,
                LanguageId = book.LanguageId,
                Language = book.Language.Name,
                Title = book.Title,
                TotalPages = book.TotalPages,
                CoverImageUrl = book.CoverImageUrl
            }).Take(count).ToListAsync();
        }

        public List<BookModel> SearchBooks(string title, string author)
        {
            //return DataSource().Where(s => s.Title == title && s.Author == author).ToList();
            return null;
        }

        public string GetAppName()
        {
            return "Book Store";
        }

        public async Task<int> UpdateBook(BookModel model)
        {
            var newbook =  _context.books.Where(b => b.Id == model.Id).Select(book => new BookModel()
            {
                Author = model.Author,
               
                Description = model.Description,
                Category = model.Category,
                Title = model.Title,
                LanguageId = (int)model.LanguageId,
                TotalPages = model.TotalPages.HasValue ? model.TotalPages.Value : 0,
               
                CoverImageUrl = model.CoverImageUrl,
                BookPdfUrl = model.BookPdfUrl
            });

             var result = _context.Entry(model).State = EntityState.Modified;
              _context.SaveChanges();
            return (int)result;
        }
        
    }
}
