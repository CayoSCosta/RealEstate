using RealEstate.Models;
using RealEstate.Models.Entities.Empreendimento;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace RealEstate.Services;

public class ImageService : IImagemService
{
    private readonly string _uploadPath;

    // Defina aqui os tamanhos que você quer gerar dinamicamente
    private readonly int[] _tamanhos = new[] { 150, 300, 600, 1200, 1920 };

    public ImageService(IWebHostEnvironment env)
    {
        _uploadPath = Path.Combine(env.WebRootPath, "uploads");
        if (!Directory.Exists(_uploadPath))
            Directory.CreateDirectory(_uploadPath);
    }

    public async Task<Imagem> ProcessarImagemAsync(IFormFile file, TipoImagem tipo, string nomeEmpreendimento, Guid? empreendimentoId = null, Guid? unidadeId = null)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("Arquivo de imagem inválido.");

        using var inputStream = file.OpenReadStream();
        IImageFormat formato = Image.DetectFormat(inputStream);
        inputStream.Position = 0;

        using var image = Image.Load(inputStream);

        string extensao = formato?.FileExtensions?.FirstOrDefault() != null
            ? $".{formato.FileExtensions.First()}"
            : ".jpg";

        var imagem = new Imagem
        {
            Tipo = tipo,
            EmpreendimentoId = empreendimentoId,
            UnidadeId = unidadeId,
            Versoes = new List<ImagemVersao>()
        };

        foreach (var largura in _tamanhos)
        {
            var (caminho, w, h) = await SalvarVersaoAsync(image, tipo, largura, extensao, nomeEmpreendimento);
            imagem.Versoes.Add(new ImagemVersao
            {
                Nome = $"{Guid.NewGuid()}_{largura}",
                Caminho = caminho,
                Extensao = extensao,
                Tamanho = $"{w}x{h}"
            });
        }

        return imagem;
    }

    private async Task<(string caminho, int width, int height)> SalvarVersaoAsync(Image img, TipoImagem tipo, int largura, string extensao, string nomeEmpreendimento)
    {
        using var clone = img.Clone(ctx => ctx.Resize(new ResizeOptions
        {
            Mode = ResizeMode.Max,
            Size = new Size(largura, 0)
        }));

        string nomeSanitizado = SanitizeFileName(nomeEmpreendimento).Replace(" ", "-").ToLower();
        string folderPath = Path.Combine(_uploadPath, nomeSanitizado, tipo.ToString());

        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        string fileName = $"{Guid.NewGuid()}_{largura}{extensao}";
        string filePath = Path.Combine(folderPath, fileName);

        await using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
        {
            await clone.SaveAsync(fs, new JpegEncoder { Quality = 90 });
        }

        return ($"/uploads/{nomeSanitizado}/{tipo}/{fileName}", clone.Width, clone.Height);
    }

    public static string SanitizeFileName(string input)
    {
        if (string.IsNullOrEmpty(input))
            return string.Empty;

        var invalidChars = Path.GetInvalidFileNameChars();
        foreach (var c in invalidChars)
            input = input.Replace(c, '_');

        return input.Trim();
    }
}
