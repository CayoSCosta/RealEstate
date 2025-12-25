using Imobi.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace Imobi.MVC.ViewModels
{
    public class ImagemViewModel
    {
        public Guid Id { get; set; }
        public string Base64 { get; set; } = string.Empty;

        [Display(Name = "Legenda")]
        public string? Legenda { get; set; }

        [Display(Name = "Ordem de Exibição")]
        public int Ordem { get; set; }

        [Display(Name = "Tipo da Imagem")]
        public TipoImagemEmpreendimentoEnum? Tipo { get; set; }

        public Guid? EmpreendimentoId { get; set; }
        public Guid? UnidadeId { get; set; }
    }
}
