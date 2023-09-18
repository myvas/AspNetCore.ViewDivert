# ViewDivert
![ViewDivert github actions status](https://github.com/myvas/AspNetCore.ViewDivert/actions/workflows/dotnet.yml/badge.svg)

A Razor ViewLocationExpander to serve dedicated views for Weixin or what-you-need browsers.

### Nuget
[Myvas.AspNetCore.ViewDivert](https://www.nuget.org/packages/Myvas.AspNetCore.ViewDivert)

### ConfigureServices()
```csharp
services.AddViewDivert();
```

### Use ViewDivert attribute
1.Put the attribute on Controller, so effect all actions in this Controller:
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

2.Put only on the specified action(s):
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

### Dev
* .NET Core SDK 2.1.505
