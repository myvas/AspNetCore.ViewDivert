﻿using Microsoft.AspNetCore.Http;

namespace Myvas.AspNetCore.ViewDivert
{

	public interface IDeviceResolver
    {
        /// <summary>
        /// Resolve the code of device from <see cref="HttpContext"/> 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        string Resolve(HttpContext context);
    }
}
