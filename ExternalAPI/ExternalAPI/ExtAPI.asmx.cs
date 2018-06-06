using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Reflection;

namespace ExternalAPI
{
    /// <summary>
    /// ExtAPI 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    [System.Web.Script.Services.ScriptService]
    public class ExtAPI : System.Web.Services.WebService
    {
        //下载Quartz 2.5版本
        //Install-Package Quartz -Version 2.5.0

       
        //{
        //    //加载数据库表的信息到字典ConcurrentDictionary中
        //    //当来调用方法的时候,如果在字典中没有找到SerialKey,就到数据库中去找,找到后就加入到字典中
        //    // 并且通过反射调用程序(反射的值加入字典中,这样的话,是不是对后面的访问要不要快一些?)
        //    //. 如在数据库中未找到报错.
        //}
        static ExtAPI()
        {
            //1.开启事务
            string Express = "0 */15 * * * ?";//每间隔15分钟执行一次
            JobSchedule.ExecuteByCron<UpdateAPIDicEnititysJob>(Express, "JobUpdateDic");
            //2.第一次加载
            APISUpLoad.LoadAPIDicEnititys();
        }
        [WebMethod]
        public string CallBackEnd(string APISerialKey, string inParamXML)
        {
            try
            {
                APIDicEnitity _APIDicEnitity = APISUpLoad.GetAPIDicEnitity(APISerialKey);

                if (null == _APIDicEnitity)//不存在,到数据库中去加载
                {
                    if (APISUpLoad.AddAPIDicEnititysByKey(APISerialKey))//从数据库中加载成功
                    {
                        //加载成功后,需要通过反射来加载方法
                        RefObject(_APIDicEnitity);
                        //调用方法
                        var _paramArray = _APIDicEnitity.API_Method.GetParameters();
                        switch (_paramArray.Length)
                        {
                            case 0:
                                return _APIDicEnitity.API_Method.Invoke(_APIDicEnitity.API_Instance, null) as string;
                            case 1:
                                return _APIDicEnitity.API_Method.Invoke(_APIDicEnitity.API_Instance, new object[] { inParamXML }) as string;
                            default:
                                Exception _ex = new Exception("调用的函数参数最多只能有1个,请前去确认...");
                                return CreateRetValEntity(_ex);
                        }
                    }
                    else//从数据库中加载失败,报错
                    {
                        Exception _ex = new Exception("您输入的APISerialKey:" + APISerialKey + "未维护或者为启用,请前去确认...");
                        return CreateRetValEntity(_ex);
                    }
                }
                else//存在
                {

                    if ((_APIDicEnitity.API_Instance == null) || (_APIDicEnitity.API_Method == null))
                    {
                        //APISerialKey 已经存在,但是是第一次调用,那么就需要反射
                        RefObject(_APIDicEnitity);
                        //调用方法
                        var _paramArray = _APIDicEnitity.API_Method.GetParameters();
                        switch (_paramArray.Length)
                        { 
                            case 0:
                                return _APIDicEnitity.API_Method.Invoke(_APIDicEnitity.API_Instance, null) as string;
                            case 1:
                                return _APIDicEnitity.API_Method.Invoke(_APIDicEnitity.API_Instance, new object[] { inParamXML }) as string;
                            default:
                                Exception _ex = new Exception("调用的函数参数最多只能有1个,请前去确认...");
                                return CreateRetValEntity(_ex);                                
                        }
                    }
                    else
                    {
                        //APISerialKey 已经存在,是第二次以上调用
                        var _paramArray = _APIDicEnitity.API_Method.GetParameters();
                        switch (_paramArray.Length)
                        {
                            case 0:
                                return _APIDicEnitity.API_Method.Invoke(_APIDicEnitity.API_Instance, null) as string;
                            case 1:
                                return _APIDicEnitity.API_Method.Invoke(_APIDicEnitity.API_Instance, new object[] { inParamXML }) as string;
                            default:
                                Exception _ex = new Exception("调用的函数参数最多只能有1个,请前去确认...");
                                return CreateRetValEntity(_ex);
                        }
                    }
                    
                }


            }
            catch (Exception ex)
            {
                return CreateRetValEntity(ex);
            }
        }
        #region 重新加载字典的值+ReLoadAPIDicEnititys
        /// <summary>
        /// 重新加载字典的值
        /// </summary>
        /// <param name="APISerialKey"></param>
        /// <returns></returns>
        [WebMethod]
        public string ReLoadAPIDicEnititys(string APISerialKey)
        {
            RetValEntity _RetValEntity = new RetValEntity();
            if (APISerialKey == "")
            {
                try
                {
                    APISUpLoad.LoadAPIDicEnititys();
                    return CreateRetValEntity(null);
                }
                catch (Exception ex)
                {
                    return CreateRetValEntity(ex);
                }
            }
            else
            {
                Exception _ex = new Exception("您输入的APISerialKey不正确...");
                return CreateRetValEntity(_ex);
            }


        }
        #endregion
        [WebMethod]
        public string HelloWorld()
        {
            //return Server.MapPath("./");返回根目录
            //return Server.MapPath("");
            //return HttpRuntime.BinDirectory;
            return "Hello World";
        }


        #region 私有函数
        /// <summary>
        /// 创建成功或者失败返回值字符串
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        private string CreateRetValEntity(Exception ex)
        {
            RetValEntity _RetValEntity = new RetValEntity();

            if (ex == null)
            {
                _RetValEntity.RetTitle = RetTitle.Success;
                _RetValEntity.ErrorMessage = string.Empty;
                _RetValEntity.Source = string.Empty;
                _RetValEntity.StackTrace = string.Empty;
                _RetValEntity.TargetSiteString = string.Empty;
            }
            else
            {

                _RetValEntity.RetTitle = RetTitle.Error;
                _RetValEntity.ErrorMessage = ex.Message;
                _RetValEntity.Source = ex.Source;
                _RetValEntity.StackTrace = ex.StackTrace;
                _RetValEntity.TargetSiteString = ex.TargetSite.ToString();

            }
            return XmlUtility.SerializeToXml(_RetValEntity);
        }
        #endregion


        private void RefObject(APIDicEnitity _APIDicEnitity)
        {

            string AssemblyPath = _APIDicEnitity.API_Path;//dll文件路径,不包含dll文件名称
            //.或..表示根目录  //都替换成 /  \替换成/
            AssemblyPath = AssemblyPath.Replace(@"//", @"\");
            AssemblyPath = AssemblyPath.Replace(@"/", @"\");
            var _pathArray = AssemblyPath.Split('\\');
            if ((_pathArray[0] == ".") || (_pathArray[0] == ".."))
            {
                AssemblyPath = AssemblyPath.Replace(@".\", "");
                AssemblyPath = AssemblyPath.Replace(@"..\", "");

                if ((!string.IsNullOrEmpty(AssemblyPath)) && (AssemblyPath.Rigth(1) != @"\")) AssemblyPath = AssemblyPath + @"\";
                AssemblyPath = HttpRuntime.BinDirectory + AssemblyPath;
            }
            
            if (string.IsNullOrEmpty(AssemblyPath)) AssemblyPath = HttpRuntime.BinDirectory + AssemblyPath;
            string AssemblyDllPath = AssemblyPath + _APIDicEnitity.API_Assemble;//dll文件全路径,包含后缀名
            if (!System.IO.File.Exists(AssemblyDllPath)) throw new Exception("文件" + AssemblyDllPath+"不存在..");//文件不存在,加载失败

            //Assembly assembly = Assembly.Load(AssemblyDllPath);//加载dll文件
            Assembly assembly = Assembly.LoadFrom(AssemblyDllPath);//加载dll文件



            string AssemblyClass = _APIDicEnitity.API_NameSpace + "." + _APIDicEnitity.API_ClassName;
            Type type = assembly.GetType(AssemblyClass);//获取类型
            if (null == type) throw new Exception("未找到类型:" + AssemblyClass);
            object oObject = Activator.CreateInstance(type);//创建实例
            if (null == oObject) throw new Exception("创建类型:" + type.ToString()+"的实例失败");
            MethodInfo _MethodInfo = type.GetMethod(_APIDicEnitity.API_FunctionName);//获取方法(函数)
            if (_MethodInfo == null)
                throw new Exception("未找到方法:" + _APIDicEnitity.API_FunctionName);

            APISUpLoad.UpDataInstanceAndMethod(_APIDicEnitity.API_SerialKey, _MethodInfo, oObject);

        }


    }
}
