using Quartz;
using QuartzMemoryLeak.Core;
using System;
using System.Threading.Tasks;

namespace QuartzMemoryLeak.Jobs.Base
{
    public abstract class AbstractJob<T> : IJob
        where T : IWorker, new()
    {
        private T _worker;
        protected T Worker
        {
            get
            {
                if (_worker == null)
                {
                    _worker = new T();
                }

                return _worker;
            }
        }

        public async Task Execute(IJobExecutionContext context)
        {
            Worker.Run();
        }
    }
}
