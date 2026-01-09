using FluentValidation;
using Imobi.Domain.Enum;
using Imobi.MVC.ViewModels.Validator.Base;

namespace Imobi.MVC.ViewModels.Validator
{
    public class EmpreendimentoValidator : BaseValidator<EmpreendimentoViewModel>
    {
        public EmpreendimentoValidator() : base()
        {
            RuleFor(x => x.Endereco).SetValidator(new EnderecoValidator());

            RuleFor(x => x.Estagio)
                .NotEqual(default(EstagioObraEnum))
                .WithMessage("Selecione um estágio de obra válido");

            RuleSet("Novo", () => {
                RuleFor(x => x.ImagensBase64)
                    .Must(x => x != null && x.Count > 0)
                    .When(x => x.Id == Guid.Empty)
                    .WithMessage("É necessário enviar ao menos uma imagem para o novo empreendimento");
            });
        }
    }
}
