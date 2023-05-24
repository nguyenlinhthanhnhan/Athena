﻿using MediatR;
using Microsoft.Extensions.Logging;

namespace Athena.Application.Commons.Behaviors;

public class UnhandledExceptionBehavior<TRequest, TResponse>:IPipelineBehavior<TRequest, TResponse>
{
    private readonly ILogger<TRequest> _logger;

    public UnhandledExceptionBehavior(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            var requestName = typeof(TRequest).Name;
            _logger.LogError(ex, "Athena Request: Unhandled Exception for Request {Name} {@Request}", requestName, request);
            throw;
        }
    }
}