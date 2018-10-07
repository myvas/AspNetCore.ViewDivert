using Microsoft.AspNetCore.Mvc;
using System;

namespace Demo.Controllers
{
    public class HomeController : Controller
    {
        [ViewDivert]
        public IActionResult Index()
        {
            return View();
        }
    }
}