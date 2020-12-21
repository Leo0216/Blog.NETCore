using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QCloud.CosApi.Common
{
    public static class Extension
    {
        public static long ToUnixTime(this DateTime nowTime)
        {
            DateTime startTime = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(1970, 1, 1, 0, 0, 0, 0), TimeZoneInfo.Local);
            return (long)Math.Round((nowTime - startTime).TotalMilliseconds, MidpointRounding.AwayFromZero);
        }
    }
}
