﻿using BookStore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public interface IBookRepository
    {
        Task<int> AddNewBook(BookModel model);
        Task<List<BookModel>> GetAllBooks();
        Task<BookModel> GetBookbyId(int id);
        Task<List<BookModel>> GetTopBooksAsync(int count);
        List<BookModel> SearchBooks(string title, string author);
        string GetAppName();
    }
}