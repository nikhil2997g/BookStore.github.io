using BookStore.Models;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private BookRepository _bookRepository = null;
        private LanguageRepository _languageRepository = null;

        public BookController(BookRepository bookRepository, LanguageRepository languageRepository)
        {
            _bookRepository = bookRepository;
            _languageRepository = languageRepository;
        }

        public async Task<ViewResult> GetAllBooks()
        {
            var data = await _bookRepository.GetAllBooks();
            return View(data);
        }

        public async Task<ViewResult> GetBook(int id)
        {
            var data = await _bookRepository.GetBookbyId(id);
            return View(data);
        }

        public List<BookModel> SearchBooks(string title, string author)
        {
            return _bookRepository.SearchBooks(title,author);
        }

        public async Task<ViewResult> AddNewBook(bool isSuccess = false, int bookId = 0)
        {
            ViewBag.IsSuccess = isSuccess;
            ViewBag.BookId = bookId;

            ViewBag.Language = new SelectList(await _languageRepository.GetLanguages(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewBook(BookModel bookModel)
        {
            if (ModelState.IsValid)
            {
                int id = await _bookRepository.AddNewBook(bookModel);
                if (id > 0)
                {
                    return RedirectToAction(nameof(AddNewBook), new { isSuccess = true, bookID = id });
                }
            }

            ViewBag.Language = new SelectList(await _languageRepository.GetLanguages(), "Id", "Name");
            return View();
        }
    }
}
