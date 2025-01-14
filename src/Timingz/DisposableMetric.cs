﻿using Perfolizer.Horology;

namespace Timingz;

internal class DisposableMetric : Metric, IDisposable, IValidatableMetric
{
    private StartedClock _startedClock = Chronometer.Start();

    internal DisposableMetric(string name, string description = null) : base(name, description)
    {
    }

    public void Dispose()
    {
        var elapsed = _startedClock.GetElapsed();
        Duration = elapsed.GetTimeValue().ToMilliseconds();
    }

    public bool Validate(out string message)
    {
        message = default;

        if (Duration.HasValue) return true;

        message = $"The disposable timing '{Name}' has not been disposed.";
        return false;
    }
}