# ViewDivert
[![GitHub (Pre-)Release Date](https://img.shields.io/github/release-date-pre/myvas/AspNetCore.ViewDivert?label=github)](https://github.com/myvas/AspNetCore.ViewDivert)
[![test](https://github.com/myvas/AspNetCore.ViewDivert/actions/workflows/dotnet.yml/badge.svg)](https://github.com/myvas/AspNetCore.ViewDivert)
[![deploy](https://github.com/myvas/AspNetCore.ViewDivert/actions/workflows/nuget.yml/badge.svg)](https://github.com/myvas/AspNetCore.ViewDivert)
[![NuGet](https://img.shields.io/nuget/v/Myvas.AspNetCore.ViewDivert.svg)](https://www.nuget.org/packages/Myvas.AspNetCore.ViewDivert)

A Razor ViewLocationExpander to serve dedicated views for Weixin or what-you-need browsers.

## Usage
### ConfigureServices()
```csharp
services.AddViewDivert();
```

### Use ViewDivert attribute
1. Put the attribute on Controller, so effect all actions in this Controller:
```csharp
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
```

2. Put only on the specified action(s):
```csharp
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
```

```
// Views/Xxx/Yyy.cshtml          (default)
// Views/Xxx/Yyy.weixin.cshtml   (MicroMessenger, via Weixin/Wechat browser)
// Views/Xxx/Yyy.tablet.cshtml   (tablet, eg. iPad, iPad Pro and etc)
// Views/Xxx/Yyy.mobile.cshtml   (mobile, via browser)
// Views/Xxx/Yyy.custom.cshtml   (custom)
```

### IDeviceResolver
```csharp
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
```

### Razor view
```csharp
@if (DeviceResolver.IsMicroMessenger(Context))
{
    <strong>This browser is/as a Weixin.</strong>
}
```

## Dev
* [Visual Studio 2022](https://visualstudio.microsoft.com)
* [.NET 8.0, 7.0, 6.0, 3.1](https://dotnet.microsoft.com/en-us/download/dotnet)
