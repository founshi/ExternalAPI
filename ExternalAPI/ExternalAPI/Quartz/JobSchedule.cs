using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExternalAPI
{

    public class JobSchedule
    {
        private static readonly ISchedulerFactory factory = null;
        private static readonly IScheduler scheduler = null;
        static JobSchedule()
        {
            factory = new StdSchedulerFactory();
            scheduler = factory.GetScheduler();
        }
       
        /// <summary>
        /// 执行JOB任务
        /// </summary>
        /// <typeparam name="T">需要执行的类</typeparam>
        /// <param name="cronExpression">执行的定时参数</param>
        /// <param name="jobName">Job名称,key值</param>
        /// <returns>true 成功, false 失败</returns>
        public static bool ExecuteByCron<T>(string cronExpression, string jobName) where T : IJob
        {
            if (string.IsNullOrEmpty(jobName.Trim())) return false;
            IJobDetail job = JobBuilder.Create<T>().Build();
            ICronTrigger trigger = (ICronTrigger)TriggerBuilder.Create().WithCronSchedule(cronExpression).Build();

            scheduler.ScheduleJob(job, trigger);
            JobKey _JobKey = job.Key;

            if (!SchedulerSession.ContainKey(jobName))
            {
                scheduler.Start();
                return SchedulerSession.AddJob(jobName, _JobKey);
            }
            else
            {
                return false;
            }

        }
        public static bool ShutDownJob(string jobName)
        {
            if (string.IsNullOrEmpty(jobName)) return false;
            JobKey _JobKey = SchedulerSession.GetJob(jobName);
            if (null != _JobKey)
            {
                scheduler.PauseJob(_JobKey);
                bool retval = scheduler.DeleteJob(_JobKey);
                if (retval)
                {
                    SchedulerSession.RemoveJob(jobName);
                }
                return retval;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 暂停JOB
        /// </summary>
        /// <param name="jobName"></param>
        /// <returns></returns>
        public static bool PauseJob(string jobName)
        {
            if (string.IsNullOrEmpty(jobName)) return false;
            JobKey _JobKey = SchedulerSession.GetJob(jobName);
            if (null != _JobKey)
            {
                scheduler.PauseJob(_JobKey);
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 重新开始JOB
        /// </summary>
        /// <param name="jobName"></param>
        /// <returns></returns>
        public static bool ResumeJob(string jobName)
        {
            if (string.IsNullOrEmpty(jobName)) return false;
            JobKey _JobKey = SchedulerSession.GetJob(jobName);

            if (null != _JobKey)
            {
                scheduler.ResumeJob(_JobKey);
                return true;
            }
            else
            {
                return false;
            }
        }

    }

}