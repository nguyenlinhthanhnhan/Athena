using System.Runtime.Serialization;
using FluentValidation.Results;

namespace Athena.Application.Commons.Exceptions;

[Serializable]
public class CustomValidationException : Exception
{
    public CustomValidationException()
        : base("One or more validation failures have occurred.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public CustomValidationException(IEnumerable<ValidationFailure> failures)
        : this()
    {
        var failureGroups = failures.GroupBy(e => e.PropertyName, e => e.ErrorMessage);

        foreach (var failureGroup in failureGroups)
        {
            var propertyName = failureGroup.Key;
            var propertyFailures = failureGroup.ToArray();

            Errors.Add(propertyName, propertyFailures);
        }
    }

    public IDictionary<string, string[]> Errors { get; }

    protected CustomValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Errors = new Dictionary<string, string[]>();
    }
}