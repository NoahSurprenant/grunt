using Microsoft.Extensions.Logging;
using System;
using System.Threading;

namespace Surprenant.Grunt.Util;

public interface IStateSeed
{
    Guid State { get; }
}

internal class StateSeed : IStateSeed
{
    private readonly ILogger<StateSeed> _logger;
    private readonly Timer _timer;
    public Guid State { get; private set; }

    public StateSeed(ILogger<StateSeed> logger)
    {
        _logger = logger;
        _timer = new Timer(Callback, null, TimeSpan.Zero, TimeSpan.FromDays(1));
    }

    private void Callback(object? state)
    {
        State = Guid.NewGuid();
        _logger.LogInformation("New State: {State}", State);
    }
}
