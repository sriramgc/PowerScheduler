using log4net;
using PowerScheduler.Service.Common;
using Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerScheduler.Service.Persistance
{
    public class FilePersistance :IPersistance
    {
        private readonly ILog _log;
        private readonly IConfigurationHelper _configHelper;

        public FilePersistance(ILog log, IConfigurationHelper configHelper)
        {
            _log = log;
            _configHelper = configHelper;
        }
        public void SaveReport(IEnumerable<PowerPeriod> aggPeriodVolume)
        {
            _log.Info("Step 2:Saving file started..");
            StringBuilder sbOutput = new StringBuilder();
            sbOutput.AppendLine(string.Join(",", "Local Time", "Volume"));
            foreach (var agp in aggPeriodVolume)
                sbOutput.AppendLine(string.Join(",", _configHelper.PowerPeriodMapper[agp.Period], agp.Volume));

            string pathName = Path.Combine(_configHelper.IntraDayFilePath,
                                            string.Format("{0}{1}.csv", _configHelper.IntraDayFileName, DateTime.Now.ToString("_yyyyMMdd_HHmm")
                  ));
            // Create and write the csv file
            File.WriteAllText(pathName, sbOutput.ToString());
            _log.Info("Step 2:Saving file done");
        }
    }
}
