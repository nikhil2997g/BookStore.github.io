using BookStore.Enums;
using BookStore.Helpers;
using Microsoft.AspNetCore.Http;
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
        //[Required(ErrorMessage = "Please Enter the Title of your Book")]
        [myCustomValidation("mvc")]
        [Required]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please Enter the Author Name")]
        public string Author { get; set; }

        [StringLength(500)]
        [Required]
        public string Description { get; set; }
        [Required]
        public string Category { get; set; }

        [Display(Name = "Language")]
        [Required(ErrorMessage = "Please Choose the Language")]
        public int? LanguageId { get; set; }
        public string Language { get; set; }

        [Display(Name = "Total Pages of Book")]
        [Required(ErrorMessage = "Please Enter the Total Pages of your Book")]
        public int? TotalPages { get; set; }
         
        [Display(Name = "Choose the Cover Photo of your Book")]
        [Required(ErrorMessage = "Please Choose your Cover Photo")]
        public IFormFile CoverPhoto { get; set; }
        public string CoverImageUrl { get; set; }

        [Display(Name = "Choose the gallery images of your Book")]
        [Required(ErrorMessage = "Please Choose your gallery images")]
        public IFormFileCollection GalleryFiles { get; set; }

        public List<GalleryModel> Gallery { get; set; }

        [Display(Name = "Choose the Pdf File of your Book")]
        [Required(ErrorMessage = "Please Choose your File")]
        public IFormFile BookPdf { get; set; }
        public string BookPdfUrl { get; set; }

        //[Display(Name = "Language")]
        //[Required]
        //public LanguageEnum LanguageEnum { get; set; }
    }
}
