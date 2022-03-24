using BookStore.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        [ViewData]
        public string Title { get; set; }
        public ViewResult Index()
        {
            var userId = _userService.GetUserId();
            var isLoggedIn = _userService.IsAuthenticated();
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
