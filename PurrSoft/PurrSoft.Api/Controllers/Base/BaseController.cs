using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PurrSoft.Api.Controllers.Base;

public abstract class BaseController : ControllerBase
{
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>()
                                                  ?? throw new InvalidOperationException("Mediator not found");
    private IMediator? _mediator;
}