using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Mvc
{
    /// <summary>
    /// 放在Controller/Action方法上，以决定是否根据浏览器来选择不同的View
    /// </summary>
    public class ViewDivertAttribute : Attribute
    {
    }
}
