﻿using MediatR;
using PurrSoft.Application.Common;
using PurrSoft.Common.Identity;

namespace PurrSoft.Infrastructure.RequestBehaviors;

public class CurrentUserBehavior<TRequest, TResponse>(ICurrentUserService currentUserService) : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        BaseRequest<TResponse> castedRequest = request as BaseRequest<TResponse>;
        castedRequest.User = await currentUserService.GetCurrentUser();
        return await next();
    }
}