using Quartz;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExternalAPI
{
    internal static class SchedulerSession
    {
        private static ConcurrentDictionary<string, JobKey> _Dic = new ConcurrentDictionary<string, JobKey>();

        public static Boolean AddJob(string vkey, JobKey vvalue)
        {
            if (string.IsNullOrEmpty(vkey)) throw new Exception("任务名称不能为空....");
            if (_Dic.ContainsKey(vkey)) throw new Exception("任务已经存在....");
            return _Dic.TryAdd(vkey, vvalue);
        }

        public static Boolean RemoveJob(string vkey)
        {
            if (string.IsNullOrEmpty(vkey)) return false;
            if (_Dic.ContainsKey(vkey))
            {
                JobKey __JobKey = null;
                return _Dic.TryRemove(vkey, out __JobKey);
            }
            else
            {
                return false;
            }

        }

        public static JobKey GetJob(string vkey)
        {
            if (string.IsNullOrEmpty(vkey)) return null;
            if (!_Dic.ContainsKey(vkey)) return null;
            return _Dic[vkey];
        }

        public static Boolean ContainKey(string vkey)
        {
            if (string.IsNullOrEmpty(vkey)) return true;
            return _Dic.ContainsKey(vkey);
        }
        public static ConcurrentDictionary<string, JobKey> GetJobs
        {
            get
            {
                return _Dic;
            }
        }

    }

}