using Microsoft.Extensions.DependencyInjection;
using Slacker.Jobs;
using Slacker.Services;

namespace Slacker
{
    internal class Program
    {
        private static async Task Main()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var jobScheduler = serviceProvider.GetRequiredService<JobScheduler>();
            await jobScheduler.StartAsync();

            await Task.Delay(-1);
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient<ISlackService, SlackService>();
            services.AddTransient<SetDisconnectedStatusJob>();
            services.AddTransient<ClearStatusJob>();
            services.AddSingleton<JobScheduler>();
        }
    }
}