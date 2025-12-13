using Imobi.Config;
using Imobi.Models.Empreendimento;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Imobi.Controllers;

[Authorize(Roles = "Admin, Gestor, Corretor, User")]
public class EmpreendimentoController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly ILogger<EmpreendimentoController> _logger;

    public EmpreendimentoController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, ILogger<EmpreendimentoController> logger)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
        _logger = logger;
    }

    // GET: Empreendimento
    public async Task<IActionResult> Index()
    {
        var applicationDbContext = await _context.Empreendimentos
            .Include(e => e.Endereco)
            .ToListAsync();

        return View(applicationDbContext);
    }

    // GET: Empreendimento/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            _logger.LogInformation($"EmpreendimentoController/Details - Parametro id é igual a nulo.");
            return NotFound();
        }            

        Empreendimento? empreendimento = await ObterEmpreendimento(id);

        if (empreendimento == null)
        {
            _logger.LogInformation("EmpreendimentoController/Details - empreendimento é igual a nulo.");
            return NotFound();
        }

        return View(empreendimento);
    }

    //GET: Empreendimento/Create
    public IActionResult Create()
    {
        ViewData["EnderecoId"] = new SelectList(_context.Enderecos, "Id", "Id");
        return View();
    }

    // POST: Empreendimento/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Empreendimento empreendimento)
    {
        _logger.LogInformation($"EmpreendimentoController/Create - Iniciando criação de empreendimento: {empreendimento.Nome}");
        try
        {
            if (ModelState.IsValid)
            {
                _context.Add(empreendimento);
                await _context.SaveChangesAsync();

                var imagensSalvas = await SalvarImagens(empreendimento);
                if (!imagensSalvas)
                    TempData["Aviso"] = "O empreendimento foi criado, mas algumas imagens não puderam ser salvas.";

                TempData["Sucesso"] = $"Empreendimento {empreendimento.Nome} criado com sucesso!";
                _logger.LogInformation($"EmpreendimentoController/Create - Empreendimento criado com sucesso: Nome: {empreendimento.Nome} ID: {empreendimento.Id}");

                return RedirectToAction(nameof(Index));
            }

            return View(empreendimento);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"EmpreendimentoController/Create - Erro ao criar empreendimento: {empreendimento.Nome} ID: {empreendimento.Id}");
            TempData["Erro"] = $"EmpreendimentoController/Create - Erro ao criar empreendimento: {empreendimento.Nome} ID: {empreendimento.Id}" + ex.Message;
            return View(empreendimento);
        }
    }

    // GET: Empreendimento/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return NotFound();

        var empreendimento = await _context.Empreendimentos
        .Include(e => e.Unidades)
        .Include(e => e.Endereco)
        .Include(e => e.Arquivos)
        .FirstOrDefaultAsync(e => e.Id == id);

        if (empreendimento == null)
            return NotFound();

        ViewData["EnderecoId"] = new SelectList(_context.Enderecos, "Id", "Id", empreendimento.EnderecoId);
        return View(empreendimento);
    }

    // POST: Empreendimento/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Empreendimento model)
    {
        if (id != model.Id)
        {
            _logger.LogWarning($"EmpreendimentoController/Edit - ID do modelo não corresponde ao ID da rota. ID Rota: {id}, ID Modelo: {model.Id}");
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            _logger.LogWarning($"EmpreendimentoController/Edit - Modelo inválido para ID: {id}");
            return View(model);
        }
        
        var original = await ObterEmpreendimento(id);

        if (original == null)
        {
            _logger.LogWarning($"EmpreendimentoController/Edit - Empreendimento original não encontrado para ID: {id}");
            return NotFound();
        }

        var (flowControl, msg) = VerificaCamposDeEdicao(model, original);
        if (!flowControl)
        {
            _logger.LogWarning($"EmpreendimentoController/Edit - {msg} Para ID: {id}");
            return BadRequest(msg);
        }

        try
        {
            _logger.LogInformation($"EmpreendimentoController/Edit - Atualizando empreendimento ID: {id}...");

            _context.Entry(original).CurrentValues.SetValues(model);

            var imagensSalvas = await SalvarImagens(original);
            if (!imagensSalvas)
                TempData["Aviso"] = "O empreendimento foi atualizado, mas algumas imagens não puderam ser salvas.";

            await _context.SaveChangesAsync();

            _logger.LogInformation($"EmpreendimentoController/Edit - Empreendimento ID: {id} atualizado com sucesso.");

            TempData["Sucesso"] = $"Empreendimento {model.Nome} ALTERADO com sucesso!";
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!EmpreendimentoExists(id))
                return NotFound();

            throw;
        }
    }

    // GET: Empreendimento/Delete/5 
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            _logger.LogInformation("EmpreendimentoController/Delete - Parametro id é igual a nulo.");
            return NotFound();
        }
            
        var empreendimento = await ObterEmpreendimento(id);

        if (empreendimento == null)
        {
            _logger.LogInformation("EmpreendimentoController/Delete - empreendimento é igual a nulo.");
            return NotFound();
        }

        return View(empreendimento);
    }

    // POST: Empreendimento/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var empreendimento = await ObterEmpreendimento(id);
        if (empreendimento != null)
        {
            _logger.LogInformation($"EmpreendimentoController/Delete - empreendimento {empreendimento.Nome} DELETADO com sucesso.");
            _context.Empreendimentos.Remove(empreendimento);
        }

        await _context.SaveChangesAsync();
        TempData["Sucesso"] = $"Empreendimento {empreendimento?.Nome} DELETADO com sucesso!";
        return RedirectToAction(nameof(Index));
    }

    private bool EmpreendimentoExists(int id)
    {
        return _context.Empreendimentos.Any(e => e.Id == id);
    }

    [HttpPost]
    public async Task<IActionResult> RemoverImagem(int id)
    {
        _logger.LogInformation($"EmpreendimentoController/RemoverImagem - Iniciando remoção de imagem. ArquivoId={id}");

        var imagem = await _context.Arquivos
            .FirstOrDefaultAsync(a => a.Id == id && a.EmpreendimentoId != null);

        if (imagem == null)
        {
            _logger.LogWarning($"EmpreendimentoController/RemoverImagem - Imagem não encontrada ou não pertence a um empreendimento. ArquivoId={id}");
            return NotFound();
        }
            
        try
        {
            // Apagar o arquivo físico
            _logger.LogInformation($"EmpreendimentoController/RemoverImagem - Removendo arquivo físico. Caminho={imagem.Caminho}");
            var caminhoFisico = Path.Combine(_webHostEnvironment.WebRootPath, imagem.Caminho.Replace("/", Path.DirectorySeparatorChar.ToString()));
            if (System.IO.File.Exists(caminhoFisico))
                System.IO.File.Delete(caminhoFisico);

            _context.Arquivos.Remove(imagem);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"EmpreendimentoController/RemoverImagem - Imagem removida com sucesso. ArquivoId={id}");
            return Ok();
        }
        catch (Exception)
        {
            _logger.LogError($"EmpreendimentoController/RemoverImagem - Erro ao remover imagem. ArquivoId={id}");
            return StatusCode(500, "Erro ao remover imagem do empreendimento.");
        }
    }

    private async Task<Empreendimento?> ObterEmpreendimento(int? id)
    {
        var empreendimento = await _context.Empreendimentos
            .Include(e => e.Endereco)
            .Include(e => e.Arquivos)
            .Include(e => e.Unidades)
            .FirstOrDefaultAsync(m => m.Id == id);

        return empreendimento;
    }

    private (bool flowControl, string? msg) VerificaCamposDeEdicao(Empreendimento model, Empreendimento original)
    {
        var camposProtegidos = new[]
        {
            nameof(Empreendimento.AreaConstruida),
            nameof(Empreendimento.SuitesTotal),
            nameof(Empreendimento.DormitoriosTotal),
            nameof(Empreendimento.BanheirosTotal),
            nameof(Empreendimento.VagasTotal)
        };

        foreach (var campo in camposProtegidos)
        {
            var valorOriginal = original.GetType().GetProperty(campo)!.GetValue(original);
            var valorNovo = model.GetType().GetProperty(campo)!.GetValue(model);

            if (!Equals(valorOriginal, valorNovo))
            {
                _logger.LogWarning($"EmpreendimentoController/Edit - Tentativa de alteração no campo protegido '{campo}'. Valor Original: {valorOriginal}, Valor Novo: {valorNovo}");
                string msg = ($"Tentativa de alteração no campo '{campo}' não permitida.");
                return (false, msg);
            }
        }

        return new(true, null);
    }

    private async Task<bool> SalvarImagens(Empreendimento empreendimento)
    {
        _logger.LogInformation($"EmpreendimentoController/SalvarImagens - Iniciando salvamento de imagens. EmpreendimentoId={empreendimento.Id}, Nome={empreendimento.Nome}");

        try
        {
            if (empreendimento.Imagens != null && empreendimento.Imagens.Any())
            {
                foreach (var imagem in empreendimento.Imagens)
                {
                    if (imagem.Length > 0)
                    {
                        var caminhoPasta = Path.Combine(_webHostEnvironment.WebRootPath, "Imagens", "Empreendimentos", empreendimento.Id.ToString());

                        if (!Directory.Exists(caminhoPasta))
                        {
                            _logger.LogInformation($"EmpreendimentoController/SalvarImagens - Criando diretório: {caminhoPasta}");
                            Directory.CreateDirectory(caminhoPasta);
                        }

                        var nomeArquivo = Path.GetFileNameWithoutExtension(imagem.FileName);
                        var extensao = Path.GetExtension(imagem.FileName);
                        var nomeFinal = $"{Guid.NewGuid()}{extensao}";
                        var caminhoArquivo = Path.Combine(caminhoPasta, nomeFinal);

                        using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
                            await imagem.CopyToAsync(stream);

                        var caminhoRelativo = Path.Combine("Imagens", "Empreendimentos", empreendimento.Id.ToString(), nomeFinal).Replace("\\", "/");

                        Arquivo arquivo = new()
                        {
                            NomeArquivo = nomeArquivo,
                            Extensao = extensao,
                            Tipo = TipoArquivo.Imagem,
                            Caminho = caminhoRelativo,
                            EmpreendimentoId = empreendimento.Id,
                            UnidadeId = null,
                            Descricao = "Imagem referente ao empreendimento."
                        };

                        _context.Arquivos.Add(arquivo);
                    }
                }

                await _context.SaveChangesAsync();

                _logger.LogInformation($"EmpreendimentoController/SalvarImagens - Imagens salvas com sucesso. EmpreendimentoId={empreendimento.Id}");
                return true;
            }

            _logger.LogWarning($"EmpreendimentoController/SalvarImagens - Nenhuma imagem enviada. EmpreendimentoId={empreendimento.Id}");

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"EmpreendimentoController/SalvarImagens - Erro ao salvar imagens. EmpreendimentoId={empreendimento.Id}, Nome={empreendimento.Nome} \n {ex.Message}");
            return false;
        }
    }
}
