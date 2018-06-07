using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Services;

namespace ExternalAPIS
{
    /// <summary>
    /// Summary description for ExternalAPIS
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class ExternalAPIS : System.Web.Services.WebService
    {
        [WebMethod]
        public string CallBackEnd(string sPName, string className, string funcName, string inParamXML)
        {
            sPName = sPName.Trim();
            className = className.Trim();
            funcName = funcName.Trim();
            if (string.IsNullOrEmpty(sPName))
            {
                return CreateRetMessage(new Exception("参数命名空间不可为空....."));
            }
            if (string.IsNullOrEmpty(className))
            {
                return CreateRetMessage(new Exception("参数类名不可为空....."));
            }
            if (string.IsNullOrEmpty(funcName))
            {
                return CreateRetMessage(new Exception("参数方法名不可为空....."));
            }
            Assembly assembly = Assembly.GetExecutingAssembly();

            //key==>命名空间+类名+方法名             
            string _key = CreateKey(sPName, className, funcName);//生成key

            if (APISUpLoad.ContainsKey(_key))
            {
                APIDicEnitity _APIDicEnitity = APISUpLoad.GetAPIDicEnitity(_key);

                var _paramArray = _APIDicEnitity.API_Method.GetParameters();
                
                switch (_paramArray.Length)
                {
                    case 0:
                        return _APIDicEnitity.API_Method.Invoke(_APIDicEnitity.API_Instance, null) as string;
                    case 1:
                        return _APIDicEnitity.API_Method.Invoke(_APIDicEnitity.API_Instance, new object[] { inParamXML }) as string;
                    default:
                        Exception _ex = new Exception("调用的函数参数最多只能有1个,请前去确认...");
                        return CreateRetMessage(_ex);
                }
            }
            else
            {
                string AssemblyClass = sPName + "." + className;
                Type type = assembly.GetType(AssemblyClass);//获取类型
                if (null == type)
                {
                    return CreateRetMessage(new Exception("未找到类型:" + AssemblyClass));
                }
                object oObject = Activator.CreateInstance(type);//创建实例

                if (null == oObject)
                {
                    return CreateRetMessage(new Exception("创建类型:" + type.ToString() + "的实例失败"));
                }
                MethodInfo _MethodInfo = type.GetMethod(funcName);//获取方法(函数)
                if (_MethodInfo == null)
                {
                    return CreateRetMessage(new Exception("未找到方法:" + funcName));
                }
                APIDicEnitity _APIDicEnitity = new APIDicEnitity();
                _APIDicEnitity.API_ClassName = className;
                _APIDicEnitity.API_FunctionName = funcName;
                _APIDicEnitity.API_Instance = oObject;
                _APIDicEnitity.API_Method = _MethodInfo;
                _APIDicEnitity.API_NameSpace = sPName;
                _APIDicEnitity.Key = _key;
                APISUpLoad.AddAPIDicEnitity(_key, _APIDicEnitity);

                var _paramArray = _APIDicEnitity.API_Method.GetParameters();

                switch (_paramArray.Length)
                {
                    case 0:
                        return _APIDicEnitity.API_Method.Invoke(_APIDicEnitity.API_Instance, null) as string;
                        
                    case 1:
                        return _APIDicEnitity.API_Method.Invoke(_APIDicEnitity.API_Instance, new object[] { inParamXML }) as string;
                    default:
                        Exception _ex = new Exception("调用的函数参数最多只能有1个,请前去确认...");
                        return CreateRetMessage(_ex);
                }
            }
        }



        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        private string CreateRetMessage(Exception ex)
        {
            RetMessage _RetMessage = new RetMessage();

            if (null == ex)
            {
                _RetMessage.RetTitle = RetTitle.Success;
                _RetMessage.ErrorMessage = string.Empty;
                _RetMessage.Source = string.Empty;
                _RetMessage.StackTrace = string.Empty;
                _RetMessage.TargetSiteString = string.Empty;
            }
            else
            {
                _RetMessage.RetTitle = RetTitle.Error;
                _RetMessage.ErrorMessage = ex.Message;
                _RetMessage.Source = ex.Source;
                _RetMessage.StackTrace = ex.StackTrace;
                _RetMessage.TargetSiteString = ex.TargetSite.ToString();

            }
            return XmlUtility.SerializeToXml(_RetMessage);
        }

        private string CreateKey(string sPName, string className, string funcName)
        {
            byte _appFlag = 4;//为不可见字符,所以输入人员无法输入这个字符
            ////=======>
            List<byte> _snlist = new List<byte>();
            _snlist.AddRange(Encoding.UTF8.GetBytes(sPName));
            _snlist.Add(_appFlag);
            sPName = Encoding.UTF8.GetString(_snlist.ToArray());
            ////=======>
            _snlist = new List<byte>();
            _snlist.AddRange(Encoding.UTF8.GetBytes(className));
            _snlist.Add(_appFlag);
            className = Encoding.UTF8.GetString(_snlist.ToArray());
            ////=======>
            _snlist = new List<byte>();
            _snlist.AddRange(Encoding.UTF8.GetBytes(funcName));
            _snlist.Add(_appFlag);
            funcName = Encoding.UTF8.GetString(_snlist.ToArray());

            string key = sPName + className + funcName;

            return key.Md5();
        }



    }
}
