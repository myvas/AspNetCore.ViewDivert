using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Myvas.AspNetCore.ViewDivert
{

    public interface IDeviceAccessor
    {
        string Device { get; }
        string Preference { get; }
    }
}
