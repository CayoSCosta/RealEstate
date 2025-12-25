using System.ComponentModel.DataAnnotations;

namespace Imobi.MVC.ViewModels
{
    public class EmpreendimentoViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Ativo")]
        public bool Status { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [Display(Name = "Nome do Empreendimento")]
        public string Nome { get; set; } = string.Empty;

        [Display(Name = "Sobre o imóvel")]
        public string Sobre { get; set; } = string.Empty;

        [Display(Name = "Área Construída (m²)")]
        public double? AreaConstruida { get; set; } // Alterado para double? (opcional)

        [Display(Name = "Estágio da Obra")]
        public string Estagio { get; set; } = string.Empty;

        // --- Atributos de Quantidade (Alterados para int? para serem opcionais) ---

        [Display(Name = "Banheiros")]
        public int? BanheirosTotal { get; set; }

        [Display(Name = "Dormitórios")]
        public int? DormitoriosTotal { get; set; }

        [Display(Name = "Suítes")]
        public int? SuitesTotal { get; set; }

        [Display(Name = "Vagas de Garagem")]
        public int? VagasTotal { get; set; }

        // --- Relacionamentos e Unidades ---
        public Guid EnderecoId { get; set; }
        public EnderecoViewModel Endereco { get; set; } = new();
        public List<UnidadeViewModel> Unidades { get; set; } = new();

        // --- Upload de imagem dinâmica ---

        [Display(Name = "Novas Imagens Recortadas")]
        public List<string> ImagensBase64 { get; set; } = new List<string>();

        [Display(Name = "Upload de Arquivos Direto")]
        public List<IFormFile> ImagensUpload { get; set; } = new List<IFormFile>();

        public List<ArquivoViewModel> Arquivos { get; set; } = new List<ArquivoViewModel>();

        public List<ImagemViewModel> Imagens { get; set; } = new List<ImagemViewModel>();

        public int TotalImagens => (Arquivos?.Count ?? 0) + (ImagensBase64?.Count ?? 0);
    }
}