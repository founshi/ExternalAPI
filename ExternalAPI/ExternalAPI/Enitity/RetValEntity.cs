using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExternalAPI
{
    public enum RetTitle
    {
        Error = 0,
        Success = 1
    }

    [Serializable]
    public class RetValEntity
    {
        /// <summary>
        /// 只有2个值 Success Error
        /// </summary>
        public RetTitle RetTitle { get; set; }
        /// <summary>
        /// 异常信息
        /// </summary>
        public string ErrorMessage { get; set; }
        /// <summary>
        /// 堆栈信息
        /// </summary>
        public string StackTrace { get; set; }
        /// <summary>
        /// 异常对象名称
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// 异常出现的方法
        /// </summary>
        public string TargetSiteString { get; set; }
    }
}