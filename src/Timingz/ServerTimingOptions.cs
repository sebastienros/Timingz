﻿using Microsoft.AspNetCore.Http;

namespace Timingz;

public class ServerTimingOptions
{
    private string _totalMetricName = DefaultTotalMetricName;

    internal const string DefaultTotalMetricName = "total";

    internal const string DefaultTotalDescription = "Total";

    public string TotalMetricName
    {
        get => _totalMetricName;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("A valid name must be provided", nameof(TotalMetricName));
            _totalMetricName = value;
        }
    }

    public string TotalMetricDescription { get; set; } = DefaultTotalDescription;

    public void WithRequestTimingOptions(Action<HttpContext, RequestTimingOptions> configure) =>
        ConfigureRequestTimingOptions = configure ?? throw new ArgumentNullException(nameof(configure));

    public IEnumerable<string> TimingAllowOrigins { get; set; } = Enumerable.Empty<string>();

    public bool InvokeCallbackServices { get; set; }

    public bool ValidateMetrics { get; set; }

    public HashSet<string> ActivitySources { get; } = new();

    internal Action<HttpContext, RequestTimingOptions> ConfigureRequestTimingOptions { get; private set; } = (_, _) => { };
}