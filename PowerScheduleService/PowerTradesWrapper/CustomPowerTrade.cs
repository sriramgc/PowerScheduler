using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerScheduler.Service.PowerTradesWrapper
{
    public class CustomPowerTrade : ICustomPowerTrade
    {
        public CustomPowerTrade() { }

        public DateTime Date { get; set; }
        public PowerPeriod[] Periods { get; set; }

    }
}
