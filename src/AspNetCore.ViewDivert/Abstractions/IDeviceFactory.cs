using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.ViewDivert
{
    public interface IDeviceFactory
    {
        IDevice Normal();
        IDevice Mobile();
        IDevice Tablet();
        IDevice Other(string code);
    }
}
