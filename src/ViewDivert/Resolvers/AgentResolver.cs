using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using Myvas.AspNetCore.ViewDivert;

namespace Microsoft.AspNetCore.Mvc
{
    public class AgentResolver : IDeviceResolver
    {
        private readonly ViewDivertOptions _options;

        public AgentResolver(IOptions<ViewDivertOptions> optionsAccessor)
        {
            _options = optionsAccessor?.Value ?? throw new ArgumentNullException(nameof(optionsAccessor));
        }

        /// <summary>
        /// 判断设备类别
        /// </summary>
        /// <param name="context"></param>
        /// <returns>未知设备类别则返回<see cref="string.Empty"/> 。</returns>
        public string Resolve(HttpContext context)
        {
            if (IsMicroMessenger(context))
                return _options.WeixinCode;

            if (IsTablet(context))
                return _options.TabletCode;

            if (IsMobile(context))
                return _options.MobileCode;

            return "";
        }

        public static bool IsMicroMessenger(HttpContext context)
        {
            try
            {
                var agent = context.Request.Headers["User-Agent"].FirstOrDefault()?.ToLowerInvariant();
                // UserAgent keyword detection of MicroMessenger
                if (agent != null && KnownMicroMessengerUserAgentKeywords.Any(keyword => agent.Contains(keyword)))
                {
                    return true;
                }
            }
            catch { }
            return false;
        }

        public static bool IsTablet(HttpContext context)
        {
            try
            {
                var agent = context.Request.Headers["User-Agent"].FirstOrDefault()?.ToLowerInvariant();
                // UserAgent keyword detection of:
                // (1) Tablet && !mobile
                // (2) ipad
                if (agent != null && KnownTabletUserAgentKeywords.Any(keyword => agent.Contains(keyword) && !agent.Contains("mobile")
                    || (agent != null && agent.Contains("ipad"))))
                {
                    return true;
                }
            }
            catch { }
            return false;
        }

        public static bool IsMobile(HttpContext context)
        {
            try
            {
                var agent = context.Request.Headers["User-Agent"].FirstOrDefault()?.ToLowerInvariant();
                // UserAgent keyword detection of:
                // (1) x-wap-profile
                // (2) profile
                if (agent != null && context.Request.Headers.ContainsKey("x-wap-profile") ||
                    context.Request.Headers.ContainsKey("Profile"))
                {
                    return true;
                }

                if (agent != null && agent.Length >= 4 && KnownMobileUserAgentPrefixes.Any(prefix => agent.StartsWith(prefix)))
                {
                    return true;
                }

                var accept = context.Request.Headers["Accept"];
                if (accept.Any(t => t.ToLowerInvariant() == "wap"))
                {
                    return true;
                }

                if (agent != null && KnownMobileUserAgentKeywords.Any(keyword => agent.Contains(keyword)))
                {
                    return true;
                }

                if (context.Request.Headers.Any(header => header.Value.Any(value => value.Contains("OperaMini"))))
                {
                    return true;
                }
            }
            catch { }
            return false;
        }

        public static readonly string[] KnownMobileUserAgentPrefixes =
        {
            "w3c ", "w3c-", "acs-", "alav", "alca", "amoi", "audi", "avan", "benq",
            "bird", "blac", "blaz", "brew", "cell", "cldc", "cmd-", "dang", "doco",
            "eric", "hipt", "htc_", "inno", "ipaq", "ipod", "jigs", "kddi", "keji",
            "leno", "lg-c", "lg-d", "lg-g", "lge-", "lg/u", "maui", "maxo", "midp",
            "mits", "mmef", "mobi", "mot-", "moto", "mwbp", "nec-", "newt", "noki",
            "palm", "pana", "pant", "phil", "play", "port", "prox", "qwap", "sage",
            "sams", "sany", "sch-", "sec-", "send", "seri", "sgh-", "shar", "sie-",
            "siem", "smal", "smar", "sony", "sph-", "symb", "t-mo", "teli", "tim-",
            "tosh", "tsm-", "upg1", "upsi", "vk-v", "voda", "wap-", "wapa", "wapi",
            "wapp", "wapr", "webc", "winw", "winw", "xda ", "xda-"
        };
        public static readonly string[] KnownMobileUserAgentKeywords =
        {
            "blackberry", "webos", "ipod", "lge vx", "midp", "maemo", "mmp", "mobile",
            "netfront", "hiptop", "nintendo DS", "novarra", "openweb", "opera mobi",
            "opera mini", "palm", "psp", "phone", "smartphone", "symbian", "up.browser",
            "up.link", "wap", "windows ce"
        };

        public static readonly string[] KnownTabletUserAgentKeywords = { "ipad", "playbook", "hp-tablet", "kindle" };

        public static readonly string[] KnownMicroMessengerUserAgentKeywords = { "micromessenger" };
    }
}
