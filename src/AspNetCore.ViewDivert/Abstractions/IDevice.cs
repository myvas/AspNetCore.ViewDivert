using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Myvas.AspNetCore.ViewDivert
{

    public interface IDevice
    {
        bool IsMobile { get; }
        bool IsTablet { get; }
        bool IsNormal { get; }
        string DeviceCode { get; }
    }
}
