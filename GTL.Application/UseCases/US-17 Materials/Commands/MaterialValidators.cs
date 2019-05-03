using System;
using FluentValidation;
using FluentValidation.Validators;

namespace GTL.Application.Validators
{
    public static class MaterialValidators
    {
        public static IRuleBuilderOptions<T, string> IsbnLength<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(m => string.IsNullOrEmpty(m) || m.Length == 10 || m.Length == 13)
                .WithMessage("'{PropertyName}' does not have a valid length");
        }

        public static IRuleBuilderOptions<T, string> StringLength<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(x => !string.IsNullOrEmpty(x) || x.Length <= 60)
                .WithMessage("'{PropertyName}' is empty or larger than 60 characters");
        }

        public static IRuleBuilderOptions<T, string> DescriptionNotEmpty<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(x => !string.IsNullOrEmpty(x)).WithMessage("'{PropertyName}' must not be empty");
        }

        public static IRuleBuilderOptions<T, int> MinNumber<T>(this IRuleBuilder<T, int> ruleBuilder)
        {
            return ruleBuilder.GreaterThanOrEqualTo(int.MinValue);
        }

        public static IRuleBuilderOptions<T, int> MaxNumber<T>(this IRuleBuilder<T, int> ruleBuilder)
        {
            return ruleBuilder.GreaterThanOrEqualTo(int.MinValue);
        }

        public static IRuleBuilderOptions<T, int> MinAndMaxNumber<T>(this IRuleBuilder<T, int> ruleBuilder)
        {
            return ruleBuilder.MaxNumber().MinNumber();
        }

        public static IRuleBuilderOptions<T, Enum> SelectedEnum<T>(this IRuleBuilder<T, Enum> ruleBuilder)
        {
            return ruleBuilder.NotNull().IsInEnum();
        }
    }
}
