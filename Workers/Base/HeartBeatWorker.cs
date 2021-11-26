using System;
using System.Diagnostics;

namespace QuartzMemoryLeak.Workers.Base
{
    public class HeartBeatWorker : Worker
    {
        protected override void Process()
        {
            Debug.WriteLine("HeartBeat");
        }
    }
}
