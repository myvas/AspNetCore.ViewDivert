using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.ViewDivertMiddleware
{
    public class ViewDivertOptions
    {
        /// <summary>
        /// View扩展方式
        /// </summary>
        public ViewDivertLocationExpanderFormat Format { get; set; } = ViewDivertLocationExpanderFormat.Suffix;

        /// <summary>
        /// View扩展识别字，<see cref="ViewLocationExpanderContext"/> 
        /// </summary>
        public string Indicator { get; set; } = ViewDivertDefaults.Indicator;

        /// <summary>
        /// View扩展码
        /// </summary>
        public string WeixinCode { get; set; } = ViewDivertDefaults.CodeOfMicroMessenger;
        public string MobileCode { get; set; } = ViewDivertDefaults.CodeOfMobile;
        public string TabletCode { get; set; } = ViewDivertDefaults.CodeOfTablet;
    }
}
