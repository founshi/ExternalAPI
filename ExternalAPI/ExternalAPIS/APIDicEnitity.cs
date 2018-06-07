using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ExternalAPIS
{
    internal class APIDicEnitity
    {
        public string Key { get; set; }
        public string API_NameSpace { get; set; }
        public string API_ClassName { get; set; }
        public string API_FunctionName { get; set; }
        public MethodInfo API_Method { get; set; }

        public object API_Instance { get; set; }
    }
}