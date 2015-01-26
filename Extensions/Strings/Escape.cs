using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions.Strings
{
    public static class Escape
    {
        public static string PathForShell(this string path)
        {
            //Quotes are a good start
            if(!path.StartsWith("\""))
            {
                path = "\"" + path;
            }
            if(!path.EndsWith("\""))
            {
                path = path + "\"";
            }

            return path
                .Replace(@"\", @"\\")
                .Replace("'", "\'");
        }
    }
}
