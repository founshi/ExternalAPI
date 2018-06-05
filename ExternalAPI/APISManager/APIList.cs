using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace APISManager
{
    /// <summary>
    /// 实体类APIList。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    
    [Serializable]
    public  class APIList
    {
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public string API_SerialKey  { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string API_Path  { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string API_Assemble  { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string API_NameSpace  { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string API_ClassName  { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string API_FunctionName  { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool API_IsUsed  { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime API_CreateTime  { get; set; }
}
}
