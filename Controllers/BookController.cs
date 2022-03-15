using BookStore.Models;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private BookRepository _bookRepository = null;
        public BookController()
        {
            _bookRepository = new BookRepository();
        }

        public ViewResult GetAllBooks()
        {
            var data = _bookRepository.GetAllBooks();
            return View(data);
        }

        public ViewResult GetBook(int id)
        {
            var data = _bookRepository.GetBookbyId(id);
            return View(data);
        }

        public List<BookModel> SearchBooks(string title, string author)
        {
            return _bookRepository.SearchBooks(title,author);
        }
    }
}
