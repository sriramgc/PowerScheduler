using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PowerScheduler.Service.PowerTradesWrapper
{
    public interface IDataService
    {
        Task<IEnumerable<CustomPowerTrade>> GetTradesAsync(DateTime asOfDate);
    }
}