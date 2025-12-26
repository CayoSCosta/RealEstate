using Microsoft.AspNetCore.Mvc.Rendering;

namespace Imobi.MVC.ViewModels.Util
{
    public class GaleriaConfigViewModel
    {
        public string Titulo { get; set; } = "Galeria de Fotos";
        public string Icone { get; set; } = "bi bi-images";
        public string ContainerId { get; set; } = "imagensContainer";
        public string ModalId { get; set; } = "modalCrop";
        public List<ImagemViewModel> Imagens { get; set; } = new();
        public IEnumerable<SelectListItem> OpcoesTipo { get; set; } = new List<SelectListItem>();
    }
}
