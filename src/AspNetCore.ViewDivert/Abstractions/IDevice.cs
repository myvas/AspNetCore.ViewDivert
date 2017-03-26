using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.ViewDivertMiddleware
{

    public interface IDevice
    {
        bool IsMobile { get; }
        bool IsTablet { get; }
        bool IsNormal { get; }
        string DeviceCode { get; }
    }
}
