using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Athena.API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public abstract class ApiController:ControllerBase
{
    private IMediator _mediator;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

}