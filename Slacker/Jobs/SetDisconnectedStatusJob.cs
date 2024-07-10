using Quartz;
using Slacker.Services;

namespace Slacker.Jobs
{
    public class SetDisconnectedStatusJob(ISlackService slackService) : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            return slackService.UpdateSlackStatus("Disconnected", ":no_entry_sign:");
        }
    }
}