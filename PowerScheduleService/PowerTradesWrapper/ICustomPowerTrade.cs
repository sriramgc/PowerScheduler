using System;
using Services;

namespace PowerScheduler.Service.PowerTradesWrapper
{
    public interface ICustomPowerTrade
    {
        DateTime Date { get; set; }
        PowerPeriod[] Periods { get; set; }
    }
}