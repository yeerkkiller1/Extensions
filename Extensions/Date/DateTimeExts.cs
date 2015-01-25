using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    public static class DateTimeExts
    {
        public static DateTime ToTimeFromUnix(this string text)
        {
            return DateTime.FromFileTime((long.Parse(text) + 11644473600) * 1000 * 1000 * 10);
        }
    }
}
