using AutoMapper;
using Imobi.Domain.Interfaces;
using Imobi.Domain.Models;
using Imobi.Domain.Models.Util;
using Imobi.MVC.ViewModels;
using Imobi.MVC.ViewModels.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Imobi.Controllers;

[Authorize]
public class EmpreendimentoController : Controller
{
    private readonly IEmpreendimentoService _empreendimentoService;
    private readonly IMapper _mapper;
    private readonly ILogger<EmpreendimentoController> _logger;

    public EmpreendimentoController(IEmpreendimentoService empreendimentoService,
                                    IMapper mapper,
                                    ILogger<EmpreendimentoController> logger)
    {
        _empreendimentoService = empreendimentoService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IActionResult> Index(QueryParameters query)
    {
        var searchParams = _mapper.Map<SearchParametersDomain>(query);
        var pagedResultDomain = await _empreendimentoService.ObterPaginado(searchParams);

        var viewModel = new PagedResultViewModel<EmpreendimentoViewModel>
        {
            Items = _mapper.Map<IEnumerable<EmpreendimentoViewModel>>(pagedResultDomain.Items),
            TotalCount = pagedResultDomain.TotalCount,
            PageNumber = pagedResultDomain.PageNumber,
            PageSize = pagedResultDomain.PageSize
        };

        return View(viewModel);
    }

    public async Task<IActionResult> Details(Guid id)
    {
        var empreendimento = await _empreendimentoService.ObterComDetalhes(id);

        if (empreendimento == null) return NotFound();

        var viewModel = _mapper.Map<EmpreendimentoViewModel>(empreendimento);

        return View(viewModel);
    }

    public async Task<IActionResult> Upsert(Guid? id)
    {
        var viewModel = new EmpreendimentoViewModel();

        if (id.HasValue && id.Value != Guid.Empty)
        {
            var empreendimento = await _empreendimentoService.ObterComDetalhes(id.Value);
            if (empreendimento == null) return NotFound();
            viewModel = _mapper.Map<EmpreendimentoViewModel>(empreendimento);
        }

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upsert(EmpreendimentoViewModel viewModel)
    {
        //Ajuste
        ModelState.Remove("Endereco.Id");
        //Ajuste 

        if (!ModelState.IsValid)
        {
            var erros = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            TempData["Erro"] = "Verifique os campos: " + string.Join(" | ", erros);
            return View(viewModel);
        }

        try
        {


            var empreendimento = _mapper.Map<Empreendimento>(viewModel);
            var isNovo = viewModel.Id == Guid.Empty;

            if (viewModel.ImagensBase64 != null && viewModel.ImagensBase64.Any())
            {
                foreach (var base64 in viewModel.ImagensBase64)
                {
                    if (string.IsNullOrEmpty(base64)) continue;

                    empreendimento.Imagens.Add(new Imagem
                    {
                        Base64 = base64,
                        Tipo = "Fachada",
                        Ordem = empreendimento.Imagens.Count + 1
                    });
                }
            }

            var listaImagensParaProcessar = empreendimento.Imagens.ToList();

            if (isNovo)
            {
                await _empreendimentoService.Adicionar(empreendimento, listaImagensParaProcessar);
                TempData["Sucesso"] = "Empreendimento cadastrado com sucesso!";
            }
            else
            {
                await _empreendimentoService.Atualizar(empreendimento, listaImagensParaProcessar);
                TempData["Sucesso"] = "Empreendimento atualizado com sucesso!";
            }

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro no Upsert");
            TempData["Erro"] = "Erro ao salvar: " + ex.Message;
            return View(viewModel);
        }
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        var empreendimento = await _empreendimentoService.ObterComDetalhes(id);
        if (empreendimento == null) return NotFound();

        var viewModel = _mapper.Map<EmpreendimentoViewModel>(empreendimento);

        return View(viewModel);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _empreendimentoService.Remover(id);

        TempData["Sucesso"] = "Empreendimento excluído com sucesso!";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> RemoverImagem(Guid id)
    {
        try
        {
            await _empreendimentoService.RemoverImagem(id);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao remover imagem via AJAX");
            return BadRequest();
        }
    }
}