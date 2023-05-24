﻿using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Athena.Application.Commons.Behaviors;

public class PerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly Stopwatch _timer;
    private readonly ILogger<TRequest> _logger;

    public PerformanceBehavior(ILogger<TRequest> logger)
    {
        _timer = new Stopwatch();
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        _timer.Start();
        var response = await next();
        _timer.Stop();

        var elapsedMilliseconds = _timer.ElapsedMilliseconds;
        if (elapsedMilliseconds <= 500) return response;

        var requestName = typeof(TRequest).Name;
        _logger.LogWarning("Travel Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@Request}",
            requestName, elapsedMilliseconds, request);
        return response;
    }
}