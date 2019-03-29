using Microsoft.AspNetCore.Mvc;
using System;

namespace Demo.Controllers
{
	public class Home2Controller : Controller
	{
		/// <summary>
		/// YES, ViewDivert on me!
		/// </summary>
		/// <returns></returns>
		[ViewDivert]
		public IActionResult Index()
        {
            return View();
        }

		/// <summary>
		/// NO, ViewDivert NOT on me!
		/// </summary>
		/// <returns></returns>
		public IActionResult About()
		{
			return View();
		}
	}
}