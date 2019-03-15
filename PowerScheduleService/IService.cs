using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace PowerScheduler.Service
{
    public interface IService
    {
        void GeneratePowerPositions(object sender, ElapsedEventArgs e);
    }
}
