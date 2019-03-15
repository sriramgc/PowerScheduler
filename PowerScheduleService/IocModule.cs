using log4net;
using Ninject.Modules;
using PowerScheduler.Service.Common;
using PowerScheduler.Service.Persistance;
using PowerScheduler.Service.PowerTradesWrapper;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerScheduler.Service
{
    public class IocModule : NinjectModule
    {
        // Bind Interfaces to implementations for dependancy injection
        public override void Load()
        {
            Bind<ILog>().ToMethod(context =>
          LogManager.GetLogger(context.Request.Target.Member.ReflectedType));
            Bind<IConfigurationHelper>().To<ConfigurationHelper>().InSingletonScope();
            Bind<IIntraDayReport>().To<IntraDayReport>().InSingletonScope();
            Bind<IPersistance>().To<FilePersistance>().InSingletonScope();
            Bind<IPowerService>().To<PowerService>().InSingletonScope();
            Bind<IDataService>().To<DataService>().InSingletonScope();
            
        }
    }
}
