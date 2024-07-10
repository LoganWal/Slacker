using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;

namespace Slacker;

public class JobFactory(IServiceProvider serviceProvider) : IJobFactory
{
    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
    {
        return serviceProvider.GetRequiredService(bundle.JobDetail.JobType) as IJob ?? throw new InvalidOperationException();
    }

    public void ReturnJob(IJob job) { }
}