using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using log4net;
using PowerScheduler.Service.Common;
using Services;
using Topshelf;
using Timer = System.Timers.Timer;

namespace PowerScheduler.Service
{
    public class IntraDayService :ServiceControl
    {
        Timer timer;
        private static object _lock = new object();
        private readonly IConfigurationHelper _configHelper;
        private readonly ILog log = LogManager.GetLogger("log");
        private readonly IIntraDayReport _intraDayReport;
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        public IntraDayService(IConfigurationHelper configHelper, IIntraDayReport intraDayReport)
        {
            timer = new Timer();
            _configHelper = configHelper;
            _intraDayReport = intraDayReport;
        }

        public void GeneratePowerPositions(object sender, ElapsedEventArgs e)
        {
            if (Monitor.TryEnter(_lock))
            {
                try
                {
                    this.RunAsync(this.cancellationTokenSource.Token).Wait();
                }
                catch (AggregateException ex)
                {
                    log.ErrorFormat("Error Occured for Signal Time: {0} {1}", e.SignalTime.ToString(), ex.Flatten().ToString());
                }
                finally
                {
                    Monitor.Exit(_lock);
                }
            }
        }
        
        

        private async Task RunAsync(CancellationToken cancellationToken)
        {
     
            
                if (!cancellationToken.IsCancellationRequested)
                {
                    Trace.TraceInformation("Calling with retries logic");
                    await Retry.Do(async () => await _intraDayReport.Generate(), TimeSpan.FromSeconds(_configHelper.RetryInMinutes * 60),_configHelper.NumberOfRetries);
                }
           
        }

        public bool Start(HostControl hostControl)
        {
            timer.Interval = _configHelper.IntervalInMinutes;

            timer.Elapsed += new ElapsedEventHandler(GeneratePowerPositions);
            timer.Start();
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            timer.Stop();
            cancellationTokenSource.Cancel();
            return true;
        }


      
    }
}
