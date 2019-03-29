using Microsoft.AspNetCore.Mvc;
using System;

namespace Demo.Controllers
{
	/// <summary>
	/// YES, ViewDivert on all actions!
	/// </summary>
	[ViewDivert]
	public class HomeController : Controller
	{
		public IActionResult Index()
        {
            return View();
        }

		public IActionResult About()
		{
			return View();
		}
	}
}