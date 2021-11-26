using QuartzMemoryLeak.Core;
using System;

namespace QuartzMemoryLeak.Workers.Base
{
    public abstract class Worker : IWorker
    {
        protected abstract void Process();

        public bool Run()
        {
            try { 

                Process();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return true;
        }
    }
}
