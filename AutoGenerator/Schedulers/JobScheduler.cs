using AutoGenerator.Config;
using Hangfire;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Spi;
using System.Reflection;
namespace AutoGenerator.Schedulers;
public class JobScheduler : IHostedService
{
    private readonly ISchedulerFactory _schedulerFactory;
    private IScheduler? _scheduler;




    private readonly Dictionary<Type, JobOptions>? _jobs;




    public JobScheduler(ISchedulerFactory schedulerFactory, Dictionary<Type, JobOptions> jobs)
    {

        _schedulerFactory = schedulerFactory;
        BackgroundJob.Schedule(
    () => Console.WriteLine("JobScheduler!"),
    TimeSpan.FromDays(7));



        _jobs = jobs;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _scheduler = await _schedulerFactory.GetScheduler();


        foreach (var infjob in _jobs)
        {
            var type = infjob.Key;


            var objob = infjob.Value;

            IJobDetail job = JobBuilder.Create(type)

             .WithIdentity(type.Name,
                 $"{objob.JobGroup}")
             .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity(
                    objob.TriggerName,
                    objob.TriggerGroup)
                .StartNow()
                .WithCronSchedule(
                    objob.Cron)
                .Build();



            await _scheduler.ScheduleJob(job, trigger);
            var jobId = BackgroundJob.Enqueue(
    () => Console.WriteLine(infjob));

            BackgroundJob.ContinueJobWith(jobId, () => Console.WriteLine("Job 2"));

        }

    }


    public async Task StopAsync(CancellationToken cancellationToken)
    {

        if (_scheduler != null)
        {
            await _scheduler.Shutdown();
        }
    }
}

