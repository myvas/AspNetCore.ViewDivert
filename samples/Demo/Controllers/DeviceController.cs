using Microsoft.AspNetCore.Mvc;
using Myvas.AspNetCore.ViewDivert;
using System;

namespace Demo.Controllers
{
	public class DeviceController : Controller
	{
		private readonly IDeviceResolver _deviceResolver;

		public DeviceController(IDeviceResolver deviceResolver)
		{
			_deviceResolver = deviceResolver;
		}

		public IActionResult Index()
		{
			var deviceCode = _deviceResolver.Resolve(HttpContext);

			ViewData["DeviceCode"] = deviceCode;

			return View();
		}
	}
}