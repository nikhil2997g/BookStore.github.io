using BookStore.Data;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class LanguageRepository
    {
        private BookStoreContext _context = null;

        public LanguageRepository(BookStoreContext context)
        {
            _context = context;
        }

        public async Task<List<LanguageModel>> GetLanguages()
        {
            return await _context.languages.Select(l => new LanguageModel()
            {
                Id = l.Id,
                Description = l.Description,
                Name = l.Name
            }).ToListAsync();
        }
    }
}
