﻿using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Athena.Application.Commons.Behaviors;

public class LoggingBehavior<TRequest> : IRequestPreProcessor<TRequest>
{
    private readonly ILogger _logger;

    public LoggingBehavior(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        _logger.LogInformation("Athena Request: {@RequestName} {@Request}", requestName, request);
    }
}