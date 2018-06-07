using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ExternalAPIS
{
    public static class StringExt
    {

        public static string Rigth(this string src, int length)
        {
            if (length <= 0) return string.Empty;
            if (src.Length <= length) return src;
            return src.Substring(src.Length - length - 1, length);
        }
        public static string Md5(this string src)
        {
            MD5 md5 = MD5.Create();
            byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(src));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].ToString("x2"));
            }
            return sb.ToString();
        }


    }
}