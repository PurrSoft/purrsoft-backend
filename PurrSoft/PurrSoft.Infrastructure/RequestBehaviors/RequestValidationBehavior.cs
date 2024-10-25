using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace PurrSoft.Infrastructure.RequestBehaviors;

public class RequestValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        ValidationContext<TRequest> context = new(request);
        List<ValidationFailure> failures = GetValidationFailures(context);
        return failures.Count != 0 ? throw new ValidationException(failures) : next();
    }

    private List<ValidationFailure> GetValidationFailures(ValidationContext<TRequest> context) =>
        validators
        .Select(v => v.Validate(context))
        .SelectMany(result => result.Errors)
        .Where(f => f != null)
        .ToList();

}