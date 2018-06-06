using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExternalAPI
{
    internal class UpdateAPIDicEnititysJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            APISUpLoad.LoadAPIDicEnititys();
        }
    }
}
////////下面是调用实例
// string cronExpression = "*/5 * * * * ?";//每天5秒执行一次
//JobSchedule _JobSchedule = new JobSchedule();
//_JobSchedule.ExecuteByCron<MyJob>(cronExpression, "jobName1");//返回的结果为True表示执行成功


//string cronExpression = "*/5 * * * * ?";//每5秒执行一次
//string cronExpression = "0 0 0 * * ?";//每天早上0点执行
//string cronExpression = "0 0 1 * * ?";//每天早上1点执行
//string cronExpression = "0 */1 * * * ?";//每隔1分钟执行一次
//string cronExpression = "0 0 1 1 * ?";//每月1号凌晨1点执行一次
//string cronExpression = "0 0 23 L * ?";//每月最后一天23点执行一次
//string cronExpression = "0 0 1 ? * L";//每周星期天凌晨1点实行一次
//string cronExpression = "0 26,29,33 * * * ?";//在26分、29分、33分执行一次
//string cronExpression = "0 0 0,13,18,21 * * ?";//每天的0点、13点、18点、21点都执行一次
//string cronExpression = "0 15 10 ? * MON-FRI";//周一至周五的上午10:15触发
//string cronExpression = "0 15 10 ? * 6L";//每月的最后一个星期五上午10:15触发 
//string cronExpression = "0 */2 * * * ";//每两个小时  
