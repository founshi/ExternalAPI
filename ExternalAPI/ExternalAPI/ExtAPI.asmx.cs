using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Web;
using System.Web.Services;

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
        //可以将这个字典做成一个类 ,开启定时更新

        //{
        //    //加载数据库表的信息到字典ConcurrentDictionary中
        //    //当来调用方法的时候,如果在字典中没有找到SerialKey,就到数据库中去找,找到后就加入到字典中
        //    // 并且通过反射调用程序(反射的值加入字典中,这样的话,是不是对后面的访问要不要快一些?)
        //    //. 如在数据库中未找到报错.
        //}
        static ExtAPI()
        {
            //1.开启事务


            //2.第一次加载
            APISUpLoad.LoadAPIDicEnititys();
        }
        [WebMethod]
        public string CallBackEnd(string APISerialKey, string inParamXML)
        {
            return null;
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
            return "Hello World";
        }
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

        


    }
}
