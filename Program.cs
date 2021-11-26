using Quartz;
using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace QuartzMemoryLeak
{
    class Program
    {
        private static IScheduler Scheduler { get; set; }


        private const int LOG_JOB_TRIGGERS_INTERVAL_IN_MSEC = 10000;

        static async Task Main(string[] args)
        {
            try
            {
                await InitQuartz();
                await LogMemoryAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                await Scheduler.Shutdown();
            }
        }

        static async Task LogMemoryAsync()
        {
            await Task.Run(() =>
            {
                while (true)
                {
                    GC.Collect();
                    var currentProcess = Process.GetCurrentProcess();
                    var totalBytesOfMemoryUsedInMb = currentProcess.WorkingSet64 / 1024;

                    Console.WriteLine($"Total In KB: {totalBytesOfMemoryUsedInMb}");
                    Thread.Sleep(LOG_JOB_TRIGGERS_INTERVAL_IN_MSEC);
                }
            });
        }

        static async Task InitQuartz()
        {
            var properties = new NameValueCollection();

            Scheduler = await SchedulerBuilder.Create(properties)
                .UseDefaultThreadPool(x => x.MaxConcurrency = 255)
                .UseXmlSchedulingConfiguration(x =>
                {
                    x.Files = new[] { "~/Jobs.Production.config" };
                    x.FailOnFileNotFound = true;
                    x.FailOnSchedulingError = true;
                })
                .BuildScheduler();

            await Scheduler.Start();
        }
    }
}
