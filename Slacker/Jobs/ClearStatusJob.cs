using Quartz;
using Slacker.Services;

namespace Slacker.Jobs
{
    public class ClearStatusJob(ISlackService slackService) : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            return slackService.UpdateSlackStatus("", "");
        }
    }
}