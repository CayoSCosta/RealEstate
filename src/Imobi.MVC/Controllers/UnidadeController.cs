using AutoMapper;
using Imobi.Application.Services;
using Imobi.Domain.Interfaces;
using Imobi.Domain.Models;
using Imobi.MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Imobi.Controllers;

[Authorize]
public class UnidadeController : Controller
{
    private readonly IUnidadeService _unidadeService;
    private readonly IEmpreendimentoService _empreendimentoService;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly ILogger<UnidadeController> _logger;

    public UnidadeController(IUnidadeService unidadeService,
                             IEmpreendimentoService empreendimentoService,
                             IMapper mapper,
                             IWebHostEnvironment webHostEnvironment,
                             ILogger<UnidadeController> logger)
    {
        _unidadeService = unidadeService;
        _empreendimentoService = empreendimentoService;
        _mapper = mapper;
        _webHostEnvironment = webHostEnvironment;
        _logger = logger;
    }

    // GET: Unidade
    public async Task<IActionResult> Index()
    {
        var unidades = await _unidadeService.ObterTodos();
        var viewModel = _mapper.Map<IEnumerable<UnidadeViewModel>>(unidades);
        return View(viewModel);
    }

    // GET: Unidade/Details/5
    public async Task<IActionResult> Details(Guid id)
    {
        var unidade = await _unidadeService.ObterPorId(id);

        if (unidade == null)
        {
            _logger.LogWarning($"Unidade não encontrada: {id}");
            return NotFound();
        }

        var viewModel = _mapper.Map<UnidadeViewModel>(unidade);
        return View(viewModel);
    }

    // GET: Unidade/Create/{empreendimentoId}
    [HttpGet("Unidade/Create/{empreendimentoId}")]
    public async Task<IActionResult> Create(Guid empreendimentoId)
    {
        // Buscamos o empreendimento para exibir o nome na tela e garantir que existe
        var empreendimento = await _empreendimentoService.ObterPorId(empreendimentoId);

        if (empreendimento == null)
        {
            TempData["Erro"] = "Empreendimento não encontrado.";
            return RedirectToAction("Index", "Empreendimento");
        }

        // Preparamos a ViewModel com o ID do pai
        var viewModel = new UnidadeViewModel
        {
            EmpreendimentoId = empreendimentoId,
            Empreendimento = _mapper.Map<EmpreendimentoViewModel>(empreendimento)

        };

        return View(viewModel);
    }

    // POST: Unidade/Create
    [HttpPost("Unidade/Create/{empreendimentoId}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(UnidadeViewModel viewModel)
    {
        if (!ModelState.IsValid) return View(viewModel);

        try
        {
            var unidade = _mapper.Map<Unidade>(viewModel);

            // O Service cuida de: Salvar Unidade, Upload de Imagens e Recalcular características do prédio
            await _unidadeService.Adicionar(unidade, viewModel.ImagensUpload, _webHostEnvironment.WebRootPath);

            TempData["Sucesso"] = "Unidade criada com sucesso!";

            // Redireciona para os detalhes do Empreendimento (fluxo mais natural)
            return RedirectToAction("Details", "Empreendimento", new { id = viewModel.EmpreendimentoId });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar unidade");
            TempData["Erro"] = "Erro ao salvar unidade.";
            return View(viewModel);
        }
    }

    // GET: Unidade/Edit/5
    public async Task<IActionResult> Edit(Guid id)
    {
        var unidade = await _unidadeService.ObterPorId(id);

        if (unidade == null) return NotFound();

        var viewModel = _mapper.Map<UnidadeViewModel>(unidade);

        return View(viewModel);
    }

    // POST: Unidade/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, UnidadeViewModel viewModel)
    {
        if (id != viewModel.Id) return NotFound();

        if (!ModelState.IsValid) return View(viewModel);

        try
        {
            var unidade = _mapper.Map<Unidade>(viewModel);

            // O Service atualiza e recalcula o prédio
            await _unidadeService.Atualizar(unidade, viewModel.ImagensUpload, _webHostEnvironment.WebRootPath);

            TempData["Sucesso"] = "Unidade atualizada com sucesso!";
            return RedirectToAction("Details", "Empreendimento", new { id = viewModel.EmpreendimentoId });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erro ao editar unidade {id}");
            TempData["Erro"] = "Erro ao atualizar unidade.";
            return View(viewModel);
        }
    }

    // GET: Unidade/Delete/5
    public async Task<IActionResult> Delete(Guid id)
    {
        var unidade = await _unidadeService.ObterPorId(id);

        if (unidade == null) return NotFound();

        var viewModel = _mapper.Map<UnidadeViewModel>(unidade);

        return View(viewModel);
    }

    // POST: Unidade/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        // Precisamos do ID do empreendimento para redirecionar de volta pra lá
        var unidade = await _unidadeService.ObterPorId(id);
        var empId = unidade?.EmpreendimentoId;

        if (unidade != null)
        {
            await _unidadeService.Remover(id);
            TempData["Sucesso"] = "Unidade removida com sucesso!";
        }

        if (empId != null)
            return RedirectToAction("Details", "Empreendimento", new { id = empId });

        return RedirectToAction(nameof(Index));
    }

    // AJAX: Remover Imagem da Unidade
    [HttpPost]
    public async Task<IActionResult> RemoverImagem(Guid id)
    {
        try
        {
            //await _unidadeService.RemoverImagem(id);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao remover imagem da unidade via AJAX");
            return BadRequest();
        }
    }
}