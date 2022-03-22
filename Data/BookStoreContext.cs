using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Data
{
    public class BookStoreContext : DbContext
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options) : base(options)
        {
        }

        public DbSet<Books> books { get; set; }

        public DbSet<BookGallery> BookGallery { get; set; }

        public DbSet<Language> languages { get; set; }
        //if we write configuration in startup.cs file using services we dont need below code
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("server=., database=BookStore, Integrated Security = true");
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
