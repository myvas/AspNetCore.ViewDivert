using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;

namespace AspNetCore.ViewDivertMiddleware
{
    public class AgentResolver : IDeviceResolver
    {
        public static bool IsMicroMessenger(HttpContext context)
        {
            var agent = context.Request.Headers["User-Agent"].FirstOrDefault()?.ToLowerInvariant();
            // UserAgent keyword detection of Normal devices
            if (agent != null && KnownMicroMessengerUserAgentKeywords.Any(keyword => agent.Contains(keyword)))
            {
                return true;
            }
            return false;
        }

        private static readonly string[] KnownMobileUserAgentPrefixes =
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
        private static readonly string[] KnownMobileUserAgentKeywords =
        {
            "blackberry", "webos", "ipod", "lge vx", "midp", "maemo", "mmp", "mobile",
            "netfront", "hiptop", "nintendo DS", "novarra", "openweb", "opera mobi",
            "opera mini", "palm", "psp", "phone", "smartphone", "symbian", "up.browser",
            "up.link", "wap", "windows ce"
        };

        private static readonly string[] KnownTabletUserAgentKeywords = { "ipad", "playbook", "hp-tablet", "kindle" };

        private static readonly string[] KnownMicroMessengerUserAgentKeywords = { "micromessenger" };

        private readonly ViewDivertOptions _options;
        public AgentResolver(IOptions<ViewDivertOptions> optionsAccessor)
        {
            if (optionsAccessor == null)
            {
                throw new ArgumentNullException(nameof(optionsAccessor));
            }
            _options = optionsAccessor.Value;
        }


        /// <summary>
        /// 判断设备类别
        /// </summary>
        /// <param name="context"></param>
        /// <returns>未知设备类别则返回<see cref="string.Empty"/> 。</returns>
        public string Resolve(HttpContext context)
        {
            var agent = context.Request.Headers["User-Agent"].FirstOrDefault()?.ToLowerInvariant();
            // UserAgent keyword detection of Normal devices
            if (agent != null && KnownMicroMessengerUserAgentKeywords.Any(keyword => agent.Contains(keyword)))
            {
                return _options.WeixinCode;
            }

            // UserAgent keyword detection of Tablet devices
            if (agent != null && KnownTabletUserAgentKeywords.Any(keyword => agent.Contains(keyword) && !agent.Contains("mobile") || (agent != null && agent.Contains("ipad"))))
            {
                return _options.TabletCode;
            }

            // UAProf detection
            if (agent != null && context.Request.Headers.ContainsKey("x-wap-profile") ||
                context.Request.Headers.ContainsKey("Profile"))
            {
                return _options.MobileCode;
            }

            // User-Agent prefix detection
            if (agent != null && agent.Length >= 4 && KnownMobileUserAgentPrefixes.Any(prefix => agent.StartsWith(prefix)))
            {
                return _options.MobileCode;
            }

            // Accept-header based detection
            var accept = context.Request.Headers["Accept"];
            if (accept.Any(t => t.ToLowerInvariant() == "wap"))
            {
                return _options.MobileCode;
            }

            // UserAgent keyword detection for Mobile devices
            if (agent != null && KnownMobileUserAgentKeywords.Any(keyword => agent.Contains(keyword)))
            {
                return _options.MobileCode;
            }

            // OperaMini special case
            if (context.Request.Headers.Any(header => header.Value.Any(value => value.Contains("OperaMini"))))
            {
                return _options.MobileCode;
            }

            return "";
        }
    }

}
