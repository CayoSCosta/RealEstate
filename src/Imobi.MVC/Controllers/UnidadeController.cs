using AutoMapper;
using Imobi.Application.Services;
using Imobi.Domain.Interfaces;
using Imobi.Domain.Models;
using Imobi.MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Imobi.Controllers
{
    [Authorize]
    public class UnidadeController : Controller
    {
        private readonly IUnidadeService _unidadeService;
        private readonly IEmpreendimentoService _empreendimentoService;
        private readonly IMapper _mapper;
        private readonly ILogger<UnidadeController> _logger;

        public UnidadeController(IUnidadeService unidadeService,
                                 IEmpreendimentoService empreendimentoService,
                                 IMapper mapper,
                                 ILogger<UnidadeController> logger)
        {
            _unidadeService = unidadeService;
            _empreendimentoService = empreendimentoService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var unidades = await _unidadeService.ObterTodos();
            var viewModel = _mapper.Map<IEnumerable<UnidadeViewModel>>(unidades);
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Upsert(Guid? id, Guid? empreendimentoId)
        {
            UnidadeViewModel viewModel;

            if (id.HasValue && id.Value != Guid.Empty)
            {
                var unidade = await _unidadeService.ObterPorId(id.Value);
                if (unidade == null) return NotFound();

                viewModel = _mapper.Map<UnidadeViewModel>(unidade);
            }
            else
            {
                viewModel = new UnidadeViewModel
                {
                    Status = true,
                    EmpreendimentoId = empreendimentoId ?? Guid.Empty
                };

                if (empreendimentoId.HasValue)
                {
                    var empreendimento = await _empreendimentoService.ObterPorId(empreendimentoId.Value);
                    if (empreendimento != null)
                    {
                        viewModel.Empreendimento = _mapper.Map<EmpreendimentoViewModel>(empreendimento);
                    }
                }
            }

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(UnidadeViewModel viewModel)
        {
            ModelState.Remove("Empreendimento");

            if (!ModelState.IsValid) return View(viewModel);

            try
            {
                var unidade = _mapper.Map<Unidade>(viewModel);

                if (viewModel.ImagensBase64 != null && viewModel.ImagensBase64.Any())
                {
                    if (unidade.Imagens == null) unidade.Imagens = new List<Imagem>();

                    foreach (var base64 in viewModel.ImagensBase64)
                    {
                        unidade.Imagens.Add(new Imagem
                        {
                            Base64 = base64,
                            Tipo = "Comum",
                            UnidadeId = unidade.Id
                        });
                    }
                }

                if (unidade.Id == Guid.Empty)
                {

                    await _unidadeService.Adicionar(unidade, null, "");
                    TempData["Sucesso"] = "Unidade cadastrada com sucesso!";
                }
                else
                {
                    await _unidadeService.Atualizar(unidade, null, "");
                    TempData["Sucesso"] = "Unidade atualizada com sucesso!";
                }

                if (unidade.EmpreendimentoId != Guid.Empty)
                {
                    return RedirectToAction("Details", "Empreendimento", new { id = unidade.EmpreendimentoId });
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao salvar unidade");
                TempData["Erro"] = "Erro: " + ex.Message;

                if (viewModel.EmpreendimentoId != Guid.Empty)
                {
                    var emp = await _empreendimentoService.ObterPorId(viewModel.EmpreendimentoId);
                    viewModel.Empreendimento = _mapper.Map<EmpreendimentoViewModel>(emp);
                }
                return View(viewModel);
            }
        }

        [HttpGet]
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

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var unidade = await _unidadeService.ObterPorId(id);
            if (unidade == null) return NotFound();
            return View(_mapper.Map<UnidadeViewModel>(unidade));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var unidade = await _unidadeService.ObterPorId(id);
            var empId = unidade?.EmpreendimentoId;

            await _unidadeService.Remover(id);

            TempData["Sucesso"] = "Unidade removida!";

            if (empId != null && empId != Guid.Empty)
                return RedirectToAction("Details", "Empreendimento", new { id = empId });

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
                _logger.LogError(ex, "Erro ao remover imagem da unidade");
                return BadRequest();
            }
        }
    }
}