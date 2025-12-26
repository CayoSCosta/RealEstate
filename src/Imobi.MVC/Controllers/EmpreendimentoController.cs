using AutoMapper;
using Imobi.Domain.Interfaces;
using Imobi.Domain.Models;
using Imobi.MVC.ViewModels;
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

    public async Task<IActionResult> Index()
    {
        var empreendimentos = await _empreendimentoService.ObterTodos();
        var viewModel = _mapper.Map<IEnumerable<EmpreendimentoViewModel>>(empreendimentos);

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
        ModelState.Remove("Endereco.Id");

        if (!ModelState.IsValid) return View(viewModel);

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