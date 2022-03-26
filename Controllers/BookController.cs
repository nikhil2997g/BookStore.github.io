using BookStore.Data;
using BookStore.Models;
using BookStore.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Route("[controller]/[action]")]
    public class BookController : Controller
    {
        private IBookRepository _bookRepository = null;
        private ILanguageRepository _languageRepository = null;
        private IWebHostEnvironment _webHostEnvironment = null;

        public BookController(IBookRepository bookRepository, ILanguageRepository languageRepository, IWebHostEnvironment webHostEnvironment)
        {
            _bookRepository = bookRepository;
            _languageRepository = languageRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<ViewResult> GetAllBooks()
        {
            var data = await _bookRepository.GetAllBooks();
            return View(data);
        }

        [Route("book-details/{id:int:min(1)}", Name = "bookDetailsRoute")]
        public async Task<ViewResult> GetBook(int id)
        {
            var data = await _bookRepository.GetBookbyId(id);
            return View(data);
        }

       

        public List<BookModel> SearchBooks(string title, string author)
        {
            return _bookRepository.SearchBooks(title,author);
        }

        [Authorize(Roles = "Admin")]
        public async Task<ViewResult> AddNewBook(bool isSuccess = false, int bookId = 0)
        {
            ViewBag.IsSuccess = isSuccess;
            ViewBag.BookId = bookId;

            //ViewBag.Language = new SelectList(await _languageRepository.GetLanguages(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewBook(BookModel bookModel)
        {
            if (ModelState.IsValid)
            {
                if (bookModel.CoverPhoto != null)
                {
                    string folder = "books/cover/";
                    bookModel.CoverImageUrl = await UploadImage(folder, bookModel.CoverPhoto);

                }

                if (bookModel.GalleryFiles != null)
                {
                    string folder = "books/gallery/";

                    bookModel.Gallery = new List<GalleryModel>();

                    foreach (var file in bookModel.GalleryFiles)
                    {
                        var gallery = new GalleryModel()
                        {
                            Name = file.FileName,
                            URL = await UploadImage(folder, file)
                        };
                        bookModel.Gallery.Add(gallery);
                    }
                }

                if (bookModel.BookPdf != null)
                {
                    string folder = "books/pdf/";
                    bookModel.BookPdfUrl = await UploadImage(folder, bookModel.BookPdf);

                }

                int id = await _bookRepository.AddNewBook(bookModel);
                if (id > 0)
                {
                    return RedirectToAction(nameof(AddNewBook), new { isSuccess = true, bookID = id });
                }
            }

            //ViewBag.Language = new SelectList(await _languageRepository.GetLanguages(), "Id", "Name");
            return View();
        }


        public async Task<ViewResult> EditBook(int id)
        {
            var data = await _bookRepository.GetBookbyId(id);
            var bookEdit = new BookModel()
            {
                Id = data.Id,
                Author = data.Author,
                Category = data.Category,
                Description = data.Description,
                LanguageId = data.LanguageId,
                Title = data.Title,
                TotalPages = data.TotalPages,
                existingCoverImagePath = data.CoverImageUrl,
                //existingGalleryImagePath = data.g,
                existingBookPdfPath = data.BookPdfUrl
            };
            return View(bookEdit);
        }

        [HttpPost]
        public async Task<IActionResult> EditBook(BookModel bookModel)
        {
            if (ModelState.IsValid)
            {
                if (bookModel.CoverPhoto != null)
                {
                    string folder = "books/cover/";

                    if (bookModel.existingCoverImagePath != null)
                    {
                        string filePath = Path.Combine(_webHostEnvironment.WebRootPath, folder, bookModel.existingCoverImagePath);
                        if ((System.IO.File.Exists(filePath)))
                        {
                            System.IO.File.Delete(filePath);
                        }
                        
                        
                    }

                    bookModel.CoverImageUrl = await UploadImage(folder, bookModel.CoverPhoto);

                }

                if (bookModel.GalleryFiles != null)
                {
                    string folder = "books/gallery/";

                    //if (bookModel.existingGalleryImagePath != null)
                    //{
                    //    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, folder, bookModel.existingGalleryImagePath);
                    //    System.IO.File.Delete(filePath);
                    //}

                    bookModel.Gallery = new List<GalleryModel>();

                    foreach (var file in bookModel.GalleryFiles)
                    {
                        var gallery = new GalleryModel()
                        {
                            Name = file.FileName,
                            URL = await UploadImage(folder, file)
                        };
                        bookModel.Gallery.Add(gallery);
                    }
                }

                if (bookModel.BookPdf != null)
                {
                    string folder = "books/pdf/";

                    if (bookModel.existingBookPdfPath != null)
                    {
                        string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "books/pdf/", bookModel.existingBookPdfPath);
                        if ((System.IO.File.Exists(filePath)))
                        {
                            System.IO.File.Delete(filePath);
                        }
                        
                    }

                    bookModel.BookPdfUrl = await UploadImage(folder, bookModel.BookPdf);

                }

                int id = await _bookRepository.UpdateBook(bookModel);
                if (id > 0)
                {
                    return RedirectToAction(nameof(EditBook), new { isSuccess = true, bookID = id });
                }
            }

            
            return View();
        }




        private async Task<string> UploadImage(string folderPath, IFormFile file)
        {
            
            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;

            //for storing image url in database i have created CoverImageUrl property in BookModel and assigned it to folder.
            

            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);

           
                await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
         
            

            return "/" + folderPath;
        }
    }
}
