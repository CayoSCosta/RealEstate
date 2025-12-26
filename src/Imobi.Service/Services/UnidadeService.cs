using Imobi.Domain.Interfaces;
using Imobi.Domain.Models;
using Imobi.Domain.Models.Util;
using Microsoft.AspNetCore.Http;

namespace Imobi.Application.Services;

public class UnidadeService : IUnidadeService
{
    private readonly IUnidadeRepository _unidadeRepo;
    private readonly IEmpreendimentoRepository _empreendimentoRepo;

    public UnidadeService(IUnidadeRepository unidadeRepo, IEmpreendimentoRepository empreendimentoRepo)
    {
        _unidadeRepo = unidadeRepo;
        _empreendimentoRepo = empreendimentoRepo;
    }

    public async Task<IEnumerable<Unidade>> ObterTodos()
    {
        return await _unidadeRepo.ObterTodos();
    }

    public async Task<PagedResult<Unidade>> ObterPaginado(SearchParametersDomain parameters)
    {
        return await _unidadeRepo.ObterPaginado(parameters);
    }

    public async Task<Unidade?> ObterPorId(Guid id)
    {
        return await _unidadeRepo.ObterPorId(id);
    }

    public async Task Remover(Guid id)
    {
        await _unidadeRepo.Remover(id);
       
    }

    public async Task Adicionar(Unidade unidade, List<IFormFile> imagens, string webRootPath)
    {

        await _unidadeRepo.Adicionar(unidade);
        await AtualizarCaracteristicasEmpreendimento(unidade.EmpreendimentoId);
    }

    public async Task Atualizar(Unidade unidade, List<IFormFile> imagens, string webRootPath)
    {

        await _unidadeRepo.Atualizar(unidade);
        await AtualizarCaracteristicasEmpreendimento(unidade.EmpreendimentoId);
    }

    private async Task AtualizarCaracteristicasEmpreendimento(Guid empreendimentoId)
    {
        var unidades = await _unidadeRepo.BuscarPorEmpreendimentoAsync(empreendimentoId);
        var emp = await _empreendimentoRepo.ObterPorId(empreendimentoId);

        if (emp == null || !unidades.Any()) return;


        await _empreendimentoRepo.Atualizar(emp);
    }

    public void Dispose()
    {
        _unidadeRepo?.Dispose();
        _empreendimentoRepo?.Dispose();
        GC.SuppressFinalize(this);
    }
}