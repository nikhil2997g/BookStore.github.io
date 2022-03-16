using BookStore.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        [StringLength(100, MinimumLength =5)]
        [Required(ErrorMessage = "Please Enter the Title of your Book")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please Enter the Author Name")]
        public string Author { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
        public string Category { get; set; }
        //[Display(Name = "Language")]
        //public string Language { get; set; }

        [Display(Name = "Total Pages of Book")]
        [Required(ErrorMessage = "Please Enter the Total Pages of your Book")]
        public int? TotalPages { get; set; }

        [Display(Name = "Language")]
        [Required]
        public LanguageEnum LanguageEnum { get; set; }
    }
}
