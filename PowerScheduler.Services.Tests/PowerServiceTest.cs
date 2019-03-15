using System;
using NUnit;
using NUnit.Framework;
using Moq;
using PowerScheduler.Service.Common;
using PowerScheduler.Service.Persistance;
using System.Collections.Generic;
using Services;
using PowerScheduler.Service.PowerTradesWrapper;
using PowerScheduler.Service;
using System.Threading.Tasks;
using System.Linq;

namespace PowerScheduler.Services.Tests
{

    public class PowerServiceTest
    {
        IConfigurationHelper _configurationHelper;
        Mock<IPersistance> _persistance;
        Mock<IDataService> _powerService;
        IEnumerable<CustomPowerTrade> _powerTrades;
        IntraDayReport _report;

        [SetUp]
        public void Initialise()
        {

            //se up data 
            _powerTrades = new List<CustomPowerTrade>()
            {
                new CustomPowerTrade(){
                    Date =DateTime.Today ,
                    Periods = new PowerPeriod[]{  new PowerPeriod() { Period = 1, Volume = 10},
                                                  new PowerPeriod(){ Period = 2, Volume = 20 }  }
                    },

                 new CustomPowerTrade(){
                    Date =DateTime.Today ,
                    Periods = new PowerPeriod[]{  new PowerPeriod() { Period = 1, Volume = 50},
                                                  new PowerPeriod(){ Period = 2, Volume = 100 }  }
                    },

            };
            SetUpMockDependencies();

           
        }

        private void SetUpMockDependencies()
        {
            _configurationHelper = new ConfigurationHelper();
            _persistance = new Mock<IPersistance>();
           

            _powerService = new Mock<IDataService>();
            

            _report = new IntraDayReport(_powerService.Object, _persistance.Object, _configurationHelper);

            



        }
        [TestCase(1, 60)]
        [TestCase(2, 120)]
        public void GivenStub_CallIntraDayReport_VerifyOutput(int period, double volume)
        {
            IEnumerable<PowerPeriod> aggPowerPeriods = new List<PowerPeriod>();
            _persistance
               .Setup(ds => ds.SaveReport(It.IsAny<IEnumerable<PowerPeriod>>()))
               .Callback((IEnumerable<PowerPeriod> da) => aggPowerPeriods = da); ;
            _powerService.Setup(s => s.GetTradesAsync(It.IsAny<DateTime>())).Returns(Task.FromResult(_powerTrades));

             _report.Generate().Wait();

            Assert.IsTrue(aggPowerPeriods.Where(s => s.Period == period).FirstOrDefault().Volume == volume);

        }
    }
}
