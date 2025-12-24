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
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly ILogger<EmpreendimentoController> _logger;

    public EmpreendimentoController(IEmpreendimentoService empreendimentoService,
                                    IMapper mapper,
                                    IWebHostEnvironment webHostEnvironment,
                                    ILogger<EmpreendimentoController> logger)
    {
        _empreendimentoService = empreendimentoService;
        _mapper = mapper;
        _webHostEnvironment = webHostEnvironment;
        _logger = logger;
    }

    // GET: Empreendimento
    public async Task<IActionResult> Index()
    {
        // 1. Service busca no banco (Entidades)
        var empreendimentos = await _empreendimentoService.ObterTodos();

        // 2. AutoMapper converte Entidades -> ViewModels
        var viewModel = _mapper.Map<IEnumerable<EmpreendimentoViewModel>>(empreendimentos);

        return View(viewModel);
    }

    // GET: Empreendimento/Details/5
    public async Task<IActionResult> Details(Guid id)
    {
        var empreendimento = await _empreendimentoService.ObterComDetalhes(id);

        if (empreendimento == null) return NotFound();

        var viewModel = _mapper.Map<EmpreendimentoViewModel>(empreendimento);

        return View(viewModel);
    }

    // GET: Empreendimento/Create
    public IActionResult Create()
    {
        return View(new EmpreendimentoViewModel());
    }

    // POST: Empreendimento/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(EmpreendimentoViewModel viewModel)
    {
        if (!ModelState.IsValid) return View(viewModel);

        try
        {
            // Converte VM -> Entidade
            var empreendimento = _mapper.Map<Empreendimento>(viewModel);

            // O Service salva no banco E processa as imagens (passamos o Path do servidor)
            await _empreendimentoService.Adicionar(empreendimento, viewModel.ImagensUpload, _webHostEnvironment.WebRootPath);

            TempData["Sucesso"] = $"Empreendimento {viewModel.Nome} criado com sucesso!";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar empreendimento");
            TempData["Erro"] = "Erro interno ao salvar.";
            return View(viewModel);
        }
    }

    // GET: Empreendimento/Edit/5
    public async Task<IActionResult> Edit(Guid id)
    {
        var empreendimento = await _empreendimentoService.ObterComDetalhes(id);

        if (empreendimento == null) return NotFound();

        var viewModel = _mapper.Map<EmpreendimentoViewModel>(empreendimento);

        return View(viewModel);
    }

    // POST: Empreendimento/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, EmpreendimentoViewModel viewModel)
    {
        if (id != viewModel.Id)
        {
            TempData["Erro"] = "ID Inconsistente";
            return RedirectToAction(nameof(Index));
        }

        if (!ModelState.IsValid) return View(viewModel);

        try
        {
            var empreendimento = _mapper.Map<Empreendimento>(viewModel);

            // Service atualiza banco e salva novas imagens se houver
            await _empreendimentoService.Atualizar(empreendimento, viewModel.ImagensUpload, _webHostEnvironment.WebRootPath);

            TempData["Sucesso"] = "Empreendimento atualizado com sucesso!";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erro ao editar ID: {id}");
            TempData["Erro"] = "Erro ao atualizar.";
            return View(viewModel);
        }
    }

    // GET: Empreendimento/Delete
    public async Task<IActionResult> Delete(Guid id)
    {
        var empreendimento = await _empreendimentoService.ObterComDetalhes(id); // Use ComDetalhes para mostrar fotos na tela de delete
        if (empreendimento == null) return NotFound();

        var viewModel = _mapper.Map<EmpreendimentoViewModel>(empreendimento);

        return View(viewModel);
    }

    // POST: Empreendimento/Delete
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        // Service remove do banco E deleta arquivos da pasta
        await _empreendimentoService.Remover(id, _webHostEnvironment.WebRootPath);

        TempData["Sucesso"] = "Empreendimento excluído com sucesso!";
        return RedirectToAction(nameof(Index));
    }

    // AJAX: Remover Imagem
    [HttpPost]
    public async Task<IActionResult> RemoverImagem(Guid id)
    {
        try
        {
            await _empreendimentoService.RemoverImagem(id, _webHostEnvironment.WebRootPath);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao remover imagem via AJAX");
            return BadRequest();
        }
    }
}