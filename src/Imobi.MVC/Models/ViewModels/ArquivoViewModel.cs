namespace Imobi.MVC.Models.ViewModels
{
    public class ArquivoViewModel
    {
        public Guid Id { get; set; }
        public string Caminho { get; set; } = string.Empty;
        public string NomeArquivo { get; set; } = string.Empty;
    }
}
