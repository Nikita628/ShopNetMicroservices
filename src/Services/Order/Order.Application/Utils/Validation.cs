using FluentValidation;
using MediatR;
using ValidationException = Order.Core.Exceptions.ValidationException;

namespace Order.Application.Utils
{
    public class Validation<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest: MediatR.IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public Validation(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators ?? throw new ArgumentNullException(nameof(validators));
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, 
            RequestHandlerDelegate<TResponse> next)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                if (failures.Count != 0)
                    throw new ValidationException(
                        failures
                            .GroupBy(f => f.PropertyName)
                            .ToDictionary(g => g.Key, g => g.Select(s => s.ErrorMessage).ToArray())
                    );
            }

            return await next();
        }
    }
}