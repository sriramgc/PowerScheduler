using log4net;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerScheduler.Service.PowerTradesWrapper
{
    /// <summary>
    /// Created this for unit testability of logic in the IntraDayReport 
    /// </summary>
    public class DataService : IDataService
    {

        private readonly PowerService _powerService = new PowerService();
        private readonly ILog log = LogManager.GetLogger("log");
        public DataService()
        {

        }
        public async Task<IEnumerable<CustomPowerTrade>> GetTradesAsync(DateTime asOfDate)
        {
            log.Info("Step 1:Calling PowerService dll's GetTrades");
            var powerTrades = await _powerService.GetTradesAsync(asOfDate);
            var customPowerTrades = powerTrades.Select(s => new CustomPowerTrade { Periods = s.Periods, Date = s.Date });
            log.Info("Step 1:Call to PowerService dll's GetTrades finished");

            return customPowerTrades;
        }
    }
}
