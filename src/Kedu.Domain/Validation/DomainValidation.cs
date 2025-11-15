using Kedu.Domain.Exceptions;

namespace Kedu.Domain.Validation
{
    public class DomainValidation
    {
        public static void NotNull(object? target, string fieldName)
        {
            if (target == null)
            {
                throw new EntityValidationException(
                    $"{fieldName} should not be null"
                );
            }
        }

        public static void NotNullOrEmpty(string? target, string fieldName)
        {
            if (String.IsNullOrWhiteSpace(target))
            {
                throw new EntityValidationException(
                    $"{fieldName} should not be empty or null"
                );
            }
        }

        public static void MinLength(string target, int minLength, string fieldName)
        {
            if (target.Length < minLength)
            {
                throw new EntityValidationException(
                    $"{fieldName} should not be less than {minLength} characters long"
                );
            }
        }

        public static void MaxLength(string target, int maxLength, string fieldName)
        {
            if (target.Length > maxLength)
            {
                throw new EntityValidationException(
                    $"{fieldName} should not be greater than {maxLength} characters long"
                );
            }
        }

        public static void GreaterThanZero(decimal number, string fieldName)
        {
            if (number <= 0)
                throw new EntityValidationException($"{fieldName} must be greater than zero");
        }

        public static void NotDefault<T>(T value, string fieldName)
        {
            if (EqualityComparer<T>.Default.Equals(value, default!))
                throw new EntityValidationException($"{fieldName} is required");
        }

        public static void ValidEnum<TEnum>(TEnum value, string fieldName) where TEnum : struct, Enum
        {
            if (!Enum.IsDefined(typeof(TEnum), value))
                throw new EntityValidationException($"{fieldName} is invalid");
        }
    }
}
