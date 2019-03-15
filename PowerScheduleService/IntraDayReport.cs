using log4net;
using PowerScheduler.Service.Common;
using PowerScheduler.Service.Persistance;
using PowerScheduler.Service.PowerTradesWrapper;
using Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerScheduler.Service
{
    public class IntraDayReport : IIntraDayReport
    {
        private readonly ILog log = LogManager.GetLogger("log");
        private readonly IDataService _powerService;
        private readonly IPersistance _persistance;
        private readonly IConfigurationHelper _configHelper;
        public IntraDayReport(IDataService powerService, IPersistance persistance, IConfigurationHelper configHelper)
        {
            _powerService = powerService;
            _persistance = persistance;
            _configHelper = configHelper;
        }

        public async Task Generate()
        {
            var mapper = _configHelper.PowerPeriodMapper;
            IEnumerable<PowerPeriod> aggPeriodVolume = await GetAggregatePowerPeriodVolume();
            _persistance.SaveReport(aggPeriodVolume);
        }

        private async Task<IEnumerable<PowerPeriod>> GetAggregatePowerPeriodVolume()
        {
            log.Info("Step 1:Calling PowerService dll's GetTrades");
            var powerTrades = await _powerService.GetTradesAsync(DateTime.Today);
            log.Info("Step 1:Call to PowerService dll's GetTrades finished");
            var aggPeriodVolume = powerTrades.SelectMany(s => s.Periods)
                .GroupBy(g => g.Period).Select(s => new PowerPeriod { Period = s.Key, Volume = s.Sum(v => v.Volume) });
            return aggPeriodVolume;
        }

        
    }
}
