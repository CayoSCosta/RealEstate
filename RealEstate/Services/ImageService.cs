using RealEstate.Config;
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

    public ImageService(IWebHostEnvironment env)
    {
        _uploadPath = Path.Combine(env.WebRootPath, "uploads");

        if (!Directory.Exists(_uploadPath))
            Directory.CreateDirectory(_uploadPath);
    }

    public async Task<Arquivo> ProcessarImagemAsync(IFormFile file, string nomeEntidade, TipoArquivo tipoArquivo, TipoEntidade tipoEntidade, Guid? entidadeId = null)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("Arquivo de imagem inválido.");

        using var inputStream = file.OpenReadStream();
        IImageFormat formato = Image.DetectFormat(inputStream);
        inputStream.Position = 0;

        using var image = Image.Load(inputStream);

        string extensao = formato?.FileExtensions?.FirstOrDefault() != null ? $".{formato.FileExtensions.First()}" : "sem extensão";

        Arquivo arquivo = new();
        arquivo.Tipo = tipoArquivo;
        arquivo.TipoEntidade = tipoEntidade;
        arquivo.EntidadeId = entidadeId;
        arquivo.NomeArquivo = Path.GetFileNameWithoutExtension(file.FileName);
        arquivo.Extensao = extensao;
        arquivo.Caminho = await SalvarArquivoAsync(file, arquivo, nomeEntidade);
        arquivo.EntidadeId = entidadeId;

        return arquivo;
    }

    private async Task<string> SalvarArquivoAsync(IFormFile file, Arquivo arquivo, string nomeEntidade)
    {
        string nomeSanitizado = SanitizeFileName(nomeEntidade).Replace(" ", "-").ToLower();
        string folderPath = Path.Combine(_uploadPath, nomeSanitizado, arquivo.Tipo.ToString());

        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        string extensao = arquivo.Extensao ?? string.Empty;
        string fileName = $"{arquivo.NomeArquivo}{extensao}";
        string filePath = Path.Combine(folderPath, fileName);
        string tipoArquivo = arquivo.Tipo.ToString();

        using var stream = new FileStream(filePath, FileMode.Create);

        await file.CopyToAsync(stream);

        return $"/uploads/{nomeSanitizado}/{tipoArquivo}/{fileName}";
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

    //private async Task<(string caminho, int width, int height)> SalvarVersaoAsync(Image img, TipoArquivo tipo, int largura, string extensao, string nomeEmpreendimento)
    //{
    //    using var clone = img.Clone(ctx => ctx.Resize(new ResizeOptions
    //    {
    //        Mode = ResizeMode.Max,
    //        Size = new Size(largura, 0)
    //    }));

    //    string nomeSanitizado = SanitizeFileName(nomeEmpreendimento).Replace(" ", "-").ToLower();
    //    string folderPath = Path.Combine(_uploadPath, nomeSanitizado, tipo.ToString());

    //    if (!Directory.Exists(folderPath))
    //        Directory.CreateDirectory(folderPath);

    //    string fileName = $"{Guid.NewGuid()}_{largura}{extensao}";
    //    string filePath = Path.Combine(folderPath, fileName);

    //    await using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
    //    {
    //        await clone.SaveAsync(fs, new JpegEncoder { Quality = 90 });
    //    }

    //    return ($"/uploads/{nomeSanitizado}/{tipo}/{fileName}", clone.Width, clone.Height);
    //}
}
