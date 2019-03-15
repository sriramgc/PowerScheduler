using log4net;
using PowerScheduler.Service.Common;
using System;

using System.Collections.Generic;

using System.Linq;

using System.Text;

using System.Threading.Tasks;

using Topshelf;
using Topshelf.Ninject;

namespace PowerScheduler.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            // log4net.Config.XmlConfigurator.Configure();
            ILog log = LogManager.GetLogger("log");
            TaskScheduler.UnobservedTaskException += (sender, e) =>
            {
                // log & alert!
                log.Error(e.Exception.Message);
                e.SetObserved();
            };
               
            try
            {
                log.Info("Hosting the service in Top shelf");



                HostFactory.Run(x => {
                    x.UseNinject(new IocModule());
                    x.Service<IntraDayService>(s =>
                    {
                        s.ConstructUsingNinject();
                        s.WhenStarted((service, hostControl) => service.Start(hostControl));
                        s.WhenStopped((service, hostControl) => service.Stop(hostControl));
                    });
                    x.RunAsLocalSystem();
                    x.SetDescription("IntraDayReport Service");
                    x.SetDisplayName("IntraDayReportService");
                    x.SetServiceName("IntraDayReportService");
                });

            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }


        }
    }
}
