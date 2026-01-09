using FluentValidation;
using Imobi.MVC.ViewModels.Validator.Base;

namespace Imobi.MVC.ViewModels.Validator
{
    public class UnidadeValidator : BaseValidator<UnidadeViewModel>
    {
        public UnidadeValidator() : base()
        {
            RuleFor(x => x.AreaConstruida)
                .GreaterThan(0).WithMessage("A área deve ser maior que zero");

            RuleFor(x => x.EmpreendimentoId)
                .NotEmpty().WithMessage("A unidade deve pertencer a um empreendimento");
        }
    }
}
