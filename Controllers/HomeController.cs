using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        [ViewData]
        public string Title { get; set; }
        public ViewResult Index()
        {
            Title = "Home";
            return View();
        }

        public ViewResult Aboutus()
        {
            
            return View();
        }

        public ViewResult Contactus()
        {
           
            return View();
        }
    }
}
