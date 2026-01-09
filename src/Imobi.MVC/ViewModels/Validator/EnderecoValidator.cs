using FluentValidation;
using Imobi.MVC.ViewModels.Validator.Base;

namespace Imobi.MVC.ViewModels.Validator
{
    public class EnderecoValidator : BaseValidator<EnderecoViewModel>
    {
        public EnderecoValidator() : base()
        {

            RuleFor(x => x.Logradouro)
                .NotEmpty()
                .WithMessage("Logradouro é preenchido automaticamente pelo CEP");
        }
    }
}
