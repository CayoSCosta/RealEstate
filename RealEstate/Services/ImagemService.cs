using RealEstate.Models;
using RealEstate.Models.Entities.Empreendimento;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace RealEstate.Services
{
    public class ImageService
    {
        private readonly string _uploadPath;

        public ImageService(IWebHostEnvironment env)
        {
            _uploadPath = Path.Combine(env.WebRootPath, "uploads");
            if (!Directory.Exists(_uploadPath))
                Directory.CreateDirectory(_uploadPath);
        }

        public async Task<Imagem> ProcessarImagemAsync(IFormFile file, TipoImagem tipo, Guid? empreendimentoId = null, Guid? unidadeId = null)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Arquivo de imagem inválido.");

            using var inputStream = file.OpenReadStream();

            // ✅ versão correta: síncrona com `out formato`
            using var image = Image.Load(inputStream, out IImageFormat formato);

            string extensao = (formato?.FileExtensions?.FirstOrDefault()) != null
                ? $".{formato.FileExtensions.First()}"
                : ".jpg";

            var imagem = new Imagem
            {
                Tipo = tipo,
                EmpreendimentoId = empreendimentoId,
                UnidadeId = unidadeId
            };

            // Thumb
            imagem.ThumbCaminho = await SalvarVersaoAsync(image, 150, "thumb", extensao);
            imagem.ThumbTamanho = $"{image.Width}x{image.Height}";
            imagem.ThumbExtensao = extensao;

            // Medium
            imagem.MediumCaminho = await SalvarVersaoAsync(image, 600, "medium", extensao);
            imagem.MediumTamanho = $"{image.Width}x{image.Height}";
            imagem.MediumExtensao = extensao;

            // Large
            imagem.LargeCaminho = await SalvarVersaoAsync(image, 1200, "large", extensao);
            imagem.LargeTamanho = $"{image.Width}x{image.Height}";
            imagem.LargeExtensao = extensao;

            // XLarge
            imagem.XLargeCaminho = await SalvarVersaoAsync(image, 1920, "xlarge", extensao);
            imagem.XLargeTamanho = $"{image.Width}x{image.Height}";
            imagem.XLargeExtensao = extensao;

            return imagem;
        }

        private async Task<string> SalvarVersaoAsync(Image img, int largura, string sufixo, string extensao)
        {
            var clone = img.Clone(x => x.Resize(new ResizeOptions
            {
                Mode = ResizeMode.Max,
                Size = new Size(largura, 0) // 0 = calcula altura proporcional
            }));

            string fileName = $"{Guid.NewGuid()}_{sufixo}{extensao}";
            string filePath = Path.Combine(_uploadPath, fileName);

            await clone.SaveAsync(filePath, new JpegEncoder
            {
                Quality = 90 // qualidade de compressão
            });

            return $"/uploads/{fileName}";
        }
    }
}
