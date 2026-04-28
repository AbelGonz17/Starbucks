using Core.mediatOR.Contracts;
using Core.MediatOR.Contracts;
using FluentValidation;
using Starbucks.Application.Execptions;

namespace Starbucks.Application.Abstractions;

public class ValiadationBehavior<TRequest, TResponse>
: IPipelineBehavior<TRequest, TResponse>
where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValiadationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request, 
        CancellationToken cancellationToken, 
        RequestHandlerDelegate<TResponse> next)
    {
        if(!_validators.Any())
            return await next();
        
        var context = new ValidationContext<TRequest>(request);

        var validationErrors = _validators
            .Select(validators => validators.Validate(context))
            .Where(validationResult => validationResult.Errors.Any())
            .SelectMany(validationResult => validationResult.Errors)
            .Select(validationFailure => new ValidationError(
                validationFailure.PropertyName,
                validationFailure.ErrorMessage
            )).ToList();
        
        if(validationErrors.Any())
            throw new Execptions.ValidationException(validationErrors);

        return await next();
    }
}