using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Myvas.AspNetCore.ViewDivert
{
    public interface IDeviceRedirector
    {
        void RedirectToDevice(HttpContext context, string code = "");
    }
}
