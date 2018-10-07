using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.ViewDivert
{

    public interface IDeviceAccessor
    {
        string Device { get; }
        string Preference { get; }
    }
}
