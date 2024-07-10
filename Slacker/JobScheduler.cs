using Quartz;
using Quartz.Impl;
using Slacker.Jobs;

namespace Slacker
{
    public class JobScheduler(IServiceProvider serviceProvider)
    {
        public async Task StartAsync()
        {
            var schedulerFactory = new StdSchedulerFactory();
            var scheduler = await schedulerFactory.GetScheduler();

            scheduler.JobFactory = new JobFactory(serviceProvider);

            var setDisconnectedStatusJob = JobBuilder.Create<SetDisconnectedStatusJob>()
                .WithIdentity("SetDisconnectedStatusJob", "group1")
                .Build();

            var clearStatusJob = JobBuilder.Create<ClearStatusJob>()
                .WithIdentity("clearStatusJob", "group1")
                .Build();

            var setDisconnectedStatusTrigger = TriggerBuilder.Create()
                .WithIdentity("setDisconnectedStatusTrigger", "group1")
                .WithDailyTimeIntervalSchedule(s => s
                    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(17, 30)))
                .Build();

            var clearStatusTrigger = TriggerBuilder.Create()
                .WithIdentity("clearStatusTrigger", "group1")
                .WithDailyTimeIntervalSchedule(s => s
                    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(8, 30)))
                .Build();

            await scheduler.ScheduleJob(setDisconnectedStatusJob, setDisconnectedStatusTrigger);
            await scheduler.ScheduleJob(clearStatusJob, clearStatusTrigger);

            await scheduler.Start();
        }
    }
}