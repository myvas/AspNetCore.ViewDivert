# AspNetCore.ViewDivert

## How to Use
### Nuget
https://www.nuget.org/packages/AspNetCore.ViewDivert

### Startup/ConfigureServices()
```csharp
services.AddViewDivert();
```

### XxxController
```csharp
public class XxxController : Controller
{
    [ViewDivert]
    public IActionResult Yyy()
    {
        return View();
    }
}

// Views/Xxx/Yyy.cshtml          (default)
// Views/Xxx/Yyy.weixin.cshtml   (MicroMessenger, aka Weixin)
// Views/Xxx/Yyy.custom.cshtml   (custom)
```

### IAgentResolver
```csharp
public class XxxController : Controller
{
    private readonly IAgentResolver _agentResolver;

    public XxxController(IAgentResolver agentResolver)
    {
        _agentResolver = agentResolver ?? throw new ArgumentNullException(nameof(agentResolver));_
    }

    public IActionResult Yyy()
    {
        var deviceCode = _agentResovler.Resolve(context);
        //...
        return View();
    }
}
```

### Razor view
```csharp
@if (AgentResolver.IsMicroMessenger(Context))
{
    <strong>This browser is/as a Weixin.</strong>
}
```

### IDE
Visual Studio 2017 15.8.2+ and .NET Core SDK v2.1.403+
