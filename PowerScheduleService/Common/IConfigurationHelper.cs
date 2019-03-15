using System.Collections.Generic;

namespace PowerScheduler.Service.Common
{
    public interface IConfigurationHelper
    {
        double IntervalInMinutes { get; }

        double RetryInMinutes { get; }

        int NumberOfRetries { get; }
        string IntraDayFilePath { get; }
        string IntraDayFileName { get; }
        string ServiceDescription { get; }
        string ServiceName { get; }

        Dictionary<int, string> PowerPeriodMapper { get; set; }
    }
}