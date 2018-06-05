using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ExternalAPI
{
    public class APIDicEnitity
    {
        public string API_SerialKey { get; set; }
        public string API_Path { get; set; }
        public string API_Assemble { get; set; }
        public string API_NameSpace { get; set; }
        public string API_ClassName { get; set; }
        public string API_FunctionName { get; set; }
        public bool API_IsUsed { get; set; }
        public MethodInfo API_Method { get; set; }

        public object API_Instance { get; set; }


    }
}