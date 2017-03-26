using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.ViewDivertMiddleware
{

    public interface IDeviceAccessor
    {
        string Device { get; }
        string Preference { get; }
    }
}
