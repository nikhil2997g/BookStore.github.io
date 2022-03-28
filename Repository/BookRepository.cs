using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
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
            //BookModel updatedBook = _bookRepository.GetBookbyId(model.Id);
            var uBook = _context.books.Where(b => b.Id == model.Id).FirstOrDefault();/*.(book => new BookModel()*/

            uBook.Author = model.Author;

            uBook.Description = model.Description;
            uBook.Category = model.Category;
            uBook.Title = model.Title;
            uBook.LanguageId = (int)model.LanguageId;
            uBook.TotalPages = model.TotalPages.HasValue ? model.TotalPages.Value : 0;
            uBook.CoverImageUrl = model.CoverImageUrl;
            uBook.BookPdfUrl = model.BookPdfUrl;
            uBook.bookGallery = new List<BookGallery>();
            foreach (var file in model.Gallery)
            {

                //uBook.bookGallery.First().Id = file.Id;
                //uBook.bookGallery.First().Name = file.Name;
                //uBook.bookGallery.First().URL = file.URL;

                uBook.bookGallery.Where(b => b.Id == model.Id).ToList().Add(new BookGallery()
                {
                    Name = file.Name,
                    URL = file.URL
                });
            }


            var bookModel = _context.books.Attach(uBook);
            bookModel.State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return uBook.Id;


            

            // var result = _context.Entry(model).State = EntityState.Modified;
            //  _context.SaveChanges();
            //return (int)result;
        }

        public async Task<int> Delete(int id)
        {
            //return await _context.books.Where(b => b.Id == id).


            var deleteBook = _context.books.Where(d => d.Id == id).FirstOrDefault();

           var result = _context.books.Remove(deleteBook);

            await _context.SaveChangesAsync();

            return deleteBook.Id;
        }
    }
}
