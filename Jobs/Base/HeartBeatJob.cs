using Quartz;
using QuartzMemoryLeak.Workers.Base;

namespace QuartzMemoryLeak.Jobs.Base
{
    [DisallowConcurrentExecution]
    public class HeartBeatJob : AbstractJob<HeartBeatWorker>
    {
    }
}
