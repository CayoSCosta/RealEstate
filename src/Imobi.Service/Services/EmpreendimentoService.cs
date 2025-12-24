using Imobi.Domain.Interfaces;
using Imobi.Domain.Models;
using Microsoft.AspNetCore.Http;
using Imobi.Domain.Enum;

namespace Imobi.Application.Services;

public class EmpreendimentoService : IEmpreendimentoService
{
    private readonly IEmpreendimentoRepository _empreendimentoRepo;
    private readonly IArquivoRepository _arquivoRepo;

    public EmpreendimentoService(IEmpreendimentoRepository empreendimentoRepo,
                                 IArquivoRepository arquivoRepo)
    {
        _empreendimentoRepo = empreendimentoRepo;
        _arquivoRepo = arquivoRepo;
    }

    public async Task<IEnumerable<Empreendimento>> ObterTodos()
    {
        return await _empreendimentoRepo.ObterTodos();
    }

    public async Task<Empreendimento?> ObterPorId(Guid id)
    {
        return await _empreendimentoRepo.ObterPorId(id);
    }

    public async Task<Empreendimento?> ObterComDetalhes(Guid id)
    {
        return await _empreendimentoRepo.ObterComDetalhesAsync(id);
    }

    public async Task Adicionar(Empreendimento empreendimento, List<IFormFile> imagens, string webRootPath)
    {

        if (imagens != null && imagens.Any())
        {
            foreach (var img in imagens)
            {
                var arquivo = await ProcessarUpload(img, webRootPath, empreendimento.Id);
                if (arquivo != null)
                {
                    empreendimento.Arquivos.Add(arquivo);
                }
            }
        }

        await _empreendimentoRepo.Adicionar(empreendimento);
    }

    public async Task Atualizar(Empreendimento empreendimento, List<IFormFile> imagens, string webRootPath)
    {
        if (imagens != null && imagens.Any())
        {
            foreach (var img in imagens)
            {
                var arquivo = await ProcessarUpload(img, webRootPath, empreendimento.Id);
                if (arquivo != null)
                {
                    empreendimento.Arquivos.Add(arquivo);
                }
            }
        }

        await _empreendimentoRepo.Atualizar(empreendimento);
    }

    public async Task Remover(Guid id, string webRootPath)
    {

        var empreendimento = await _empreendimentoRepo.ObterComDetalhesAsync(id);

        if (empreendimento != null && empreendimento.Arquivos.Any())
        {
            foreach (var arquivo in empreendimento.Arquivos)
            {
                ApagarArquivoDoDisco(arquivo.Caminho, webRootPath);
            }
        }

        await _empreendimentoRepo.Remover(id);
    }

    public async Task RemoverImagem(Guid arquivoId, string webRootPath)
    {
        var arquivo = await _arquivoRepo.ObterPorId(arquivoId);
        if (arquivo == null) return;

        ApagarArquivoDoDisco(arquivo.Caminho, webRootPath);

        await _arquivoRepo.Remover(arquivoId);
    }

    private async Task<Arquivo?> ProcessarUpload(IFormFile imagem, string webRootPath, Guid empreendimentoId)
    {
        if (imagem.Length == 0) return null;

        var caminhoRelativoPasta = Path.Combine("Imagens", "Empreendimentos", empreendimentoId.ToString());
        var caminhoFisicoPasta = Path.Combine(webRootPath, caminhoRelativoPasta);

        if (!Directory.Exists(caminhoFisicoPasta))
            Directory.CreateDirectory(caminhoFisicoPasta);

        var extensao = Path.GetExtension(imagem.FileName);
        var nomeArquivoNovo = $"{Guid.NewGuid()}{extensao}";
        var caminhoFisicoArquivo = Path.Combine(caminhoFisicoPasta, nomeArquivoNovo);

        using (var stream = new FileStream(caminhoFisicoArquivo, FileMode.Create))
        {
            await imagem.CopyToAsync(stream);
        }

        return new Arquivo
        {
            NomeArquivo = imagem.FileName,
            Caminho = Path.Combine(caminhoRelativoPasta, nomeArquivoNovo).Replace("\\", "/"),
            Extensao = extensao,
            Tipo = TipoArquivoEnum.Imagem,
            EmpreendimentoId = empreendimentoId
        };
    }

    private void ApagarArquivoDoDisco(string caminhoRelativo, string webRootPath)
    {
        try
        {
            if (string.IsNullOrEmpty(caminhoRelativo)) return;

            var caminhoFisico = Path.Combine(webRootPath, caminhoRelativo.Replace("/", Path.DirectorySeparatorChar.ToString()));

            if (File.Exists(caminhoFisico))
            {
                File.Delete(caminhoFisico);
            }
        }
        catch
        {

        }
    }

    public void Dispose()
    {
        _empreendimentoRepo?.Dispose();
        _arquivoRepo?.Dispose();
    }
}