namespace Imobi.MVC.Models.Components
{
    public class ModalCropperOptions
    {
        public string ModalId { get; set; } = "modalCropperGeneric";
        public string Title { get; set; } = "Editar Imagem";
        public bool EnableAvatars { get; set; } = false;
        public string TargetImageId { get; set; }
    }
}
