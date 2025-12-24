using Imobi.Domain.Enum;

namespace Imobi.MVC.ViewModels
{
    public class ArquivoViewModel
    {
        public Guid Id { get; set; }
        public string Caminho { get; set; } = string.Empty;
        public string NomeArquivo { get; set; } = string.Empty;
        public TipoArquivoEnum Tipo { get; set; }
    }
}
