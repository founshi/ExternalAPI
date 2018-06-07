using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ExternalAPIS
{
    internal static class APISUpLoad
    {
        private static ConcurrentDictionary<string, APIDicEnitity> _ConcurrentDictionary = new ConcurrentDictionary<string, APIDicEnitity>();

        public static void AddAPIDicEnitity(string _key, APIDicEnitity _APIDicEnitity)
        {
            if (!_ConcurrentDictionary.ContainsKey(_key))
                _ConcurrentDictionary.TryAdd(_key,_APIDicEnitity);
        }

        public static void UpDateAPIDicEnitity(string _key, MethodInfo API_Method, object API_Instance)
        {
            if (_ConcurrentDictionary.ContainsKey(_key))
            {
                _ConcurrentDictionary[_key].API_Method = API_Method;
                _ConcurrentDictionary[_key].API_Instance = API_Instance;
            }
        }

        public static Boolean ContainsKey(string _key)
        {
            return _ConcurrentDictionary.ContainsKey(_key);
        }

        public static APIDicEnitity GetAPIDicEnitity(string _key)
        {
            if (!_ConcurrentDictionary.ContainsKey(_key))
            {
                return null;
            }
            else
            {
                return _ConcurrentDictionary[_key];
            }
        }




    }
}