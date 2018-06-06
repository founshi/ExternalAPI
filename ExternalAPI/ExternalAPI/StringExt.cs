using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExternalAPI
{
    public static class StringExt
    {

        public static string Rigth(this string src, int length)
        {
            if (length <= 0) return string.Empty;
            if (src.Length <= length) return src;
            return src.Substring(src.Length - length-1, length);
        }
    }
}