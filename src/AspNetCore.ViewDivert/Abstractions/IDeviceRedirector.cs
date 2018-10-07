using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.ViewDivert
{
    public interface IDeviceRedirector
    {
        void RedirectToDevice(HttpContext context, string code = "");
    }
}
