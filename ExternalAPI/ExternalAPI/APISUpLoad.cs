using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ExternalAPI
{
    internal static class APISUpLoad
    {
        //可以让后台事务15分钟更新一次
        private static ConcurrentDictionary<string, APIDicEnitity> _ConcurrentDictionary = new ConcurrentDictionary<string, APIDicEnitity>();
        private static CoreAPIList _CoreAPIList = new CoreAPIList();

        #region 获取Key值对应的APIDicEnitity+ APIDicEnitity GetAPIDicEnitity
        /// <summary>
        /// 获取Key值对应的APIDicEnitity
        /// </summary>
        /// <param name="API_SerialKey">Key值</param>
        /// <returns></returns>
        public static APIDicEnitity GetAPIDicEnitity(string API_SerialKey)
        {
            if (_ConcurrentDictionary.ContainsKey(API_SerialKey))
            {
                return _ConcurrentDictionary[API_SerialKey];
            }
            else
            {
                return null;
            }
        }
        #endregion
        #region 更新反射出来的值+void UpDataInstanceAndMethod
        /// <summary>
        /// 更新 反射出来的值
        /// </summary>
        /// <param name="API_SerialKey"></param>
        /// <param name="API_Method"></param>
        /// <param name="API_Instance"></param>
        public static void UpDataInstanceAndMethod(string API_SerialKey, MethodInfo API_Method, object API_Instance)
        {
            if (_ConcurrentDictionary.ContainsKey(API_SerialKey))
            {
                _ConcurrentDictionary[API_SerialKey].API_Method = API_Method;
                _ConcurrentDictionary[API_SerialKey].API_Instance = API_Instance;
            }
        }

        #endregion
        #region 更新字典的信息+ void LoadAPIDicEnititys
        /// <summary>
        /// 更新字典的信息 _ConcurrentDictionary
        /// </summary>
        public static void LoadAPIDicEnititys()
        {

            var __mlist = _CoreAPIList.LoadEntities(t => t.API_IsUsed == true).Select
                (t => new APIDicEnitity
                {
                    API_Assemble = t.API_Assemble,
                    API_ClassName = t.API_ClassName,
                    API_FunctionName = t.API_FunctionName,
                    //API_Instance = null,
                    API_IsUsed = t.API_IsUsed,
                    //API_Method = null,//默认值就是NULL
                    API_NameSpace = t.API_NameSpace,
                    API_Path = t.API_Path,
                    API_SerialKey = t.API_SerialKey
                }).ToList();            

            ConcurrentDictionary<string, APIDicEnitity> __ConcurrentDictionary = new ConcurrentDictionary<string, APIDicEnitity>();
            for (int i = 0; i < __mlist.Count; i++)
            {
                if (_ConcurrentDictionary.ContainsKey(__mlist[i].API_SerialKey))
                {
                    __mlist[i].API_Instance = _ConcurrentDictionary[__mlist[i].API_SerialKey].API_Instance;
                    __mlist[i].API_Method = _ConcurrentDictionary[__mlist[i].API_SerialKey].API_Method;
                }
                __ConcurrentDictionary.TryAdd(__mlist[i].API_SerialKey, __mlist[i]);
            }

            
            _ConcurrentDictionary = __ConcurrentDictionary;
        }
        #endregion
        #region 根据传入的API_SerialKey来新增字典+bool AddAPIDicEnititysByKey
        /// <summary>
        /// 根据传入的API_SerialKey来新增字典
        /// </summary>
        /// <param name="API_SerialKey"></param>
        /// <returns></returns>
        public static bool AddAPIDicEnititysByKey(string API_SerialKey)
        {
            var __mlist = _CoreAPIList.LoadEntities(null).Where(t => t.API_IsUsed == true && t.API_SerialKey == API_SerialKey).Select(t => new APIDicEnitity
            {
                API_Assemble = t.API_Assemble,
                API_ClassName = t.API_ClassName,
                API_FunctionName = t.API_FunctionName,
                API_Instance = null,
                API_IsUsed = t.API_IsUsed,
                API_Method = null,
                API_NameSpace = t.API_NameSpace,
                API_Path = t.API_Path,
                API_SerialKey = t.API_SerialKey
            }).ToList();

            if (__mlist.Count == 0)
            {
                _ConcurrentDictionary.TryAdd(__mlist[0].API_SerialKey, __mlist[0]);
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion


    }
}