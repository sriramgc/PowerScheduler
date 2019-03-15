using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerScheduler.Service.Persistance
{
    public interface IPersistance
    {
        void SaveReport(IEnumerable<PowerPeriod> aggregatePowerPeriods);
    }
}
