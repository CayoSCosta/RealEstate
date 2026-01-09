using FluentValidation;
using System.Reflection;

namespace Imobi.MVC.ViewModels.Validator.Base
{
    public abstract class BaseValidator<T> : AbstractValidator<T>
    {
        protected BaseValidator()
        {
            var properties = typeof(T).GetProperties();

            foreach (var prop in properties)
            {
                var attr = prop.GetCustomAttribute<ValidateAsAttribute>();
                if (attr == null) continue;

                var propertyName = prop.Name;

                switch (attr.Type)
                {
                    case ValidationType.Title:
                        RuleFor(x => prop.GetValue(x) as string)
                            .NotEmpty().WithMessage($"{propertyName} é obrigatório")
                            .MinimumLength(ValidationLimits.TitleMin)
                            .WithMessage($"{propertyName} deve ter no mínimo {ValidationLimits.TitleMin} caracteres")
                            .MaximumLength(ValidationLimits.TitleMax)
                            .WithMessage($"{propertyName} deve ter no máximo {ValidationLimits.TitleMax} caracteres")
                            .Must(v => v != null && !v.Contains("  "))
                            .WithMessage($"{propertyName} não pode conter espaços duplos")
                            .Matches(@"^[A-Z].*$")
                            .WithMessage($"{propertyName} deve começar com letra maiúscula");
                        break;

                    case ValidationType.CEP:
                        RuleFor(x => prop.GetValue(x) as string)
                            .NotEmpty().WithMessage("CEP obrigatório")
                            .Matches(@"^\d{5}-?\d{3}$")
                            .WithMessage("Formato de CEP inválido");
                        break;

                    case ValidationType.Currency:
                        RuleFor(x => prop.GetValue(x))
                            .Must(v => v != null && decimal.TryParse(v.ToString(), out decimal res) && res > 0)
                            .WithMessage($"{propertyName} deve ser um valor maior que zero");
                        break;

                    case ValidationType.Number:
                        RuleFor(x => prop.GetValue(x) == null ? string.Empty : prop.GetValue(x)!.ToString())
                            .NotEmpty().WithMessage($"{propertyName} é obrigatório")
                            .MaximumLength(ValidationLimits.NumberMaxDigits)
                            .WithMessage($"{propertyName} não pode exceder {ValidationLimits.NumberMaxDigits} dígitos")
                            .Matches(@"^\d+$")
                            .WithMessage($"{propertyName} aceita apenas números");
                        break;

                    case ValidationType.Email:
                        RuleFor(x => prop.GetValue(x) as string)
                            .NotEmpty().WithMessage("E-mail é obrigatório")
                            .EmailAddress().WithMessage("E-mail inválido");
                        break;

                    case ValidationType.Text:
                        RuleFor(x => prop.GetValue(x) as string)
                            .NotEmpty().WithMessage($"{propertyName} não pode ser vazio")
                            .MaximumLength(ValidationLimits.TextMax)
                            .WithMessage($"{propertyName} deve ter no máximo {ValidationLimits.TextMax} caracteres");
                        break;


                    case ValidationType.TextBox:
                        RuleFor(x => prop.GetValue(x) as string)
                            .MaximumLength(ValidationLimits.TextBoxMax)
                            .WithMessage($"{propertyName} deve ter no máximo {ValidationLimits.TextBoxMax} caracteres");
                        break;
                }
            }
        }
    }
}