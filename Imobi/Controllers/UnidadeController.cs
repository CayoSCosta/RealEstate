using Imobi.Config;
using Imobi.Models.Empreendimento;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Imobi.Controllers;

[Authorize(Roles = "Admin, Gestor, Corretor, User")]
public class UnidadeController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly ILogger<UnidadeController> _logger;

    public UnidadeController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, ILogger<UnidadeController> logger)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
        _logger = logger;
    }

    // GET: Unidade
    public IActionResult Index()
    {
        var unidades = _context.Unidades.Include(u => u.Empreendimento);
        return View(unidades);
    }

    // GET: Unidade/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            _logger.LogInformation($"UnidadeController/Details - Parametro id é igual a nulo.");
            return NotFound();
        }

        var unidade = await _context.Unidades
            .Include(u => u.Empreendimento)
            .Include(u => u.Arquivos)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (unidade == null)
        {
            _logger.LogInformation($"UnidadeController/Details - unidade é igual a nulo.");
            return NotFound();
        }

        return View(unidade);
    }

    // GET: Unidade/Create
    public IActionResult Create()
    {
        ViewData["EmpreendimentoId"] = new SelectList(_context.Empreendimentos, "Id", "Nome");
        return View();
    }

    // GET: Unidade/Create/{empreendimentoId}
    [HttpGet("Unidade/Create/{empreendimentoId}")]
    public IActionResult Create(int empreendimentoId)
    {
        var empreendimento = _context.Empreendimentos
            .FirstOrDefault(e => e.Id == empreendimentoId);

        if (empreendimento == null)
        {
            _logger.LogInformation($"UnidadeController/Create - empreendimento é igual a nulo.");
            return NotFound();
        }


        ViewBag.EmpreendimentoNome = empreendimento.Nome;
        ViewBag.EmpreendimentoId = empreendimento.Id;

        return View(new Unidade { EmpreendimentoId = empreendimentoId });
    }

    // POST: Unidade/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Unidade unidade)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogInformation($"UnidadeController/Create - ModelState não é válido.");
            return View(unidade);
        }               

        await _context.AddAsync(unidade);
        await _context.SaveChangesAsync();

        await SalvarImagens(unidade);
        await AtualizarCaracteristicasDeUnidadesAsync(unidade.EmpreendimentoId);

        _logger.LogInformation($"UnidadeController/Create - Unidade ID:{unidade.Id} CRIADA com sucesso!");
        TempData["Sucesso"] = "Unidade CRIADA com sucesso!";
        return RedirectToAction(nameof(Index));
    }

    // GET: Unidade/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            _logger.LogInformation($"UnidadeController/Edit - o parâmetro id é igual a nullo");
            return NotFound();
        }


        var unidade = await _context.Unidades
            .Include(u => u.Arquivos)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (unidade == null)
        {
            _logger.LogInformation($"UnidadeController/Edit - unidade é igual a nullo");
            return NotFound();
        }

        ViewData["EmpreendimentoId"] = new SelectList(_context.Empreendimentos, "Id", "Id", unidade.EmpreendimentoId);
        return View(unidade);
    }

    // POST: Unidade/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Unidade unidade)
    {
        _logger.LogInformation($"UnidadeController/Edit - Iniciando edição da Unidade ID:{id}");

        if (id != unidade.Id)
        {
            _logger.LogInformation($"UnidadeController/Edit - ID da rota ({id}) é diferente do ID da entidade ({unidade.Id}).");
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            _logger.LogInformation($"UnidadeController/Edit - ModelState inválido para Unidade ID:{id}");
            return View(unidade);
        }

        try
        {
            _logger.LogInformation($"UnidadeController/Edit - Atualizando entidade Unidade ID:{id} no contexto.");
            _context.Update(unidade);

            await AtualizarCaracteristicasDeUnidadesAsync(unidade.EmpreendimentoId);
            await SalvarImagens(unidade);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"UnidadeController/Edit - Unidade ID:{id} ATUALIZADA com sucesso!");
            TempData["Sucesso"] = $"Unidade ATUALIZADA com sucesso!";
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogError(ex, $"UnidadeController/Edit - ERRO de concorrência ao atualizar Unidade ID:{id}");

            if (!UnidadeExists(unidade.Id))
            {
                _logger.LogInformation($"UnidadeController/Edit - Unidade ID:{id} não encontrada durante tratamento de concorrência.");
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"UnidadeController/Edit - ERRO inesperado ao atualizar Unidade ID:{id}");
            throw;
        }
    }

    // GET: Unidade/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            _logger.LogInformation($"UnidadeController/Delete - parâmetro id é igual a nulo");
            return NotFound();
        }

        var unidade = await _context.Unidades
            .Include(u => u.Empreendimento)
            .Include(u => u.Arquivos)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (unidade == null)
        {
            _logger.LogInformation($"UnidadeController/Delete - unidade é igual a nulo");
            return NotFound();
        }

        return View(unidade);
    }

    // POST: Unidade/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var unidade = await ObterUnidade(id);

        if (unidade != null)
        {
            _logger.LogInformation($"UnidadeController/DeleteConfirmed - parâmetro id é igual a nulo");
            _context.Unidades.Remove(unidade);
        }                

        await _context.SaveChangesAsync();

        _logger.LogInformation($"UnidadeController/DeleteConfirmed - Unidade ID:{unidade!.Id} DELETADA com sucesso!");
        TempData["Sucesso"] = $"Unidade DELETADA com sucesso!";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> RemoverImagem(int id)
    {
        _logger.LogInformation($"UnidadeController/RemoverImagem - Iniciando remoção da imagem ID:{id}");

        var imagem = await _context.Arquivos
            .FirstOrDefaultAsync(a => a.Id == id && a.EmpreendimentoId != null);

        if (imagem == null)
        {
            _logger.LogInformation($"UnidadeController/RemoverImagem - Imagem ID:{id} não encontrada no banco.");
            return NotFound();
        }

        try
        {
            // Caminho do arquivo físico
            var caminhoFisico = Path.Combine(_webHostEnvironment.WebRootPath, imagem.Caminho.Replace("/", Path.DirectorySeparatorChar.ToString()));

            _logger.LogInformation($"UnidadeController/RemoverImagem - Caminho do arquivo físico: {caminhoFisico}");

            // Apagar arquivo físico se existir
            if (System.IO.File.Exists(caminhoFisico))
            {
                System.IO.File.Delete(caminhoFisico);
                _logger.LogInformation($"UnidadeController/RemoverImagem - Arquivo físico removido com sucesso para imagem ID:{id}");
            }
            else
            {
                _logger.LogInformation($"UnidadeController/RemoverImagem - Arquivo físico não encontrado para imagem ID:{id}. Removendo apenas do banco.");
            }

            // Remover do banco
            _context.Arquivos.Remove(imagem);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"UnidadeController/RemoverImagem - Registro removido do banco com sucesso para imagem ID:{id}");

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"UnidadeController/RemoverImagem - ERRO ao remover imagem ID:{id}");
            return StatusCode(500, "Erro ao remover imagem do empreendimento.");
        }
    }

    private bool UnidadeExists(int id)
    {
        return _context.Unidades.Any(e => e.Id == id);
    }

    private async Task<Unidade?> ObterUnidade(int? id)
    {
        return await _context.Unidades
            .Include(e => e.Empreendimento)
            .Include(e => e.Arquivos)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    private async Task<Empreendimento?> AtualizarCaracteristicasDeUnidadesAsync(int empreendimentoId)
    {
        var emp = await _context.Empreendimentos
            .Include(e => e.Unidades)
            .FirstOrDefaultAsync(e => e.Id == empreendimentoId);

        if (emp == null)
        {
            _logger.LogInformation($"UnidadeController/AtualizarCaracteristicasDeUnidadesAsync - Empreendimento ID:{empreendimentoId} não encontrado.");
            return null;
        }

        var unidades = emp.Unidades;

        // Se não tem unidades, zera tudo
        if (unidades == null || !unidades.Any())
        {
            _logger.LogInformation($"UnidadeController/AtualizarCaracteristicasDeUnidadesAsync - Empreendimento ID:{empreendimentoId} não possui unidades. Zerando características.");

            emp.AreaConstruida = "0 m²";
            emp.BanheirosTotal = "0";
            emp.DormitoriosTotal = "0";
            emp.SuitesTotal = "0";
            emp.VagasTotal = "0";

            await _context.SaveChangesAsync();

            _logger.LogInformation($"UnidadeController/AtualizarCaracteristicasDeUnidadesAsync - Características zeradas com sucesso para Empreendimento ID:{empreendimentoId}");
            return emp;
        }

        try
        {
            _logger.LogInformation($"UnidadeController/AtualizarCaracteristicasDeUnidadesAsync - Calculando ranges de características para {unidades.Count} unidades do Empreendimento ID:{empreendimentoId}");

            // Funções auxiliares
            static string Range(int min, int max) => min == max ? $"{min}" : $"{min} - {max}";
            static string RangeArea(int min, int max) => min == max ? $"{min} m²" : $"{min} - {max} m²";

            emp.BanheirosTotal = Range(unidades.Min(u => u.Banheiros), unidades.Max(u => u.Banheiros));
            emp.DormitoriosTotal = Range(unidades.Min(u => u.Dormitorios), unidades.Max(u => u.Dormitorios));
            emp.SuitesTotal = Range(unidades.Min(u => u.Suites), unidades.Max(u => u.Suites));
            emp.VagasTotal = Range(unidades.Min(u => u.Vagas), unidades.Max(u => u.Vagas));

            emp.AreaConstruida = RangeArea(
                unidades.Min(u => u.AreaConstruida),
                unidades.Max(u => u.AreaConstruida)
            );

            await _context.SaveChangesAsync();

            _logger.LogInformation($"UnidadeController/AtualizarCaracteristicasDeUnidadesAsync - Características do Empreendimento ID:{empreendimentoId} atualizadas com sucesso!");

            return emp;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"UnidadeController/AtualizarCaracteristicasDeUnidadesAsync - ERRO ao atualizar características do Empreendimento ID:{empreendimentoId}");
            throw;
        }
    }

    private async Task SalvarImagens(Unidade unidade)
    {
        _logger.LogInformation($"UnidadeController/SalvarImagens - Iniciando salvamento de imagens para a Unidade ID:{unidade.Id}");

        if (unidade.Imagens == null || !unidade.Imagens.Any())
        {
            _logger.LogInformation($"UnidadeController/SalvarImagens - Nenhuma imagem enviada para a Unidade ID:{unidade.Id}");
            return;
        }

        foreach (var imagem in unidade.Imagens)
        {
            if (imagem.Length > 0)
            {
                try
                {
                    string empreendimentoId = unidade.EmpreendimentoId.ToString();
                    string unidadeId = unidade.Id.ToString();

                    // verifica pasta empreendimento
                    var caminhoPastaEmpreendimento = Path.Combine(_webHostEnvironment.WebRootPath, "Imagens", "Empreendimentos", empreendimentoId);
                    if (!Directory.Exists(caminhoPastaEmpreendimento))
                    {
                        Directory.CreateDirectory(caminhoPastaEmpreendimento);
                        _logger.LogInformation($"UnidadeController/SalvarImagens - Pasta criada: {caminhoPastaEmpreendimento}");
                    }

                    // verifica pasta unidade
                    var caminhoPastaUnidade = Path.Combine(caminhoPastaEmpreendimento, unidadeId);
                    if (!Directory.Exists(caminhoPastaUnidade))
                    {
                        Directory.CreateDirectory(caminhoPastaUnidade);
                        _logger.LogInformation($"UnidadeController/SalvarImagens - Pasta criada: {caminhoPastaUnidade}");
                    }

                    var nomeArquivo = Path.GetFileNameWithoutExtension(imagem.FileName);
                    var extensao = Path.GetExtension(imagem.FileName);
                    var nomeFinal = $"{Guid.NewGuid()}{extensao}";
                    var caminhoArquivo = Path.Combine(caminhoPastaUnidade, nomeFinal);

                    _logger.LogInformation($"UnidadeController/SalvarImagens - Salvando imagem '{imagem.FileName}' como '{nomeFinal}' no caminho: {caminhoArquivo}");

                    using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
                        await imagem.CopyToAsync(stream);

                    var caminhoRelativo = Path.Combine("Imagens", "Empreendimentos", empreendimentoId, unidadeId, nomeFinal).Replace("\\", "/");

                    Arquivo arquivo = new()
                    {
                        NomeArquivo = nomeArquivo,
                        Extensao = extensao,
                        Tipo = TipoArquivo.Imagem,
                        Caminho = caminhoRelativo,
                        Descricao = "Teste descrição de imagem unidade",
                        EmpreendimentoId = int.Parse(empreendimentoId),
                        UnidadeId = int.Parse(unidadeId),
                    };

                    _context.Arquivos.Add(arquivo);

                    _logger.LogInformation($"UnidadeController/SalvarImagens - Registro criado para imagem '{nomeFinal}' no banco.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"UnidadeController/SalvarImagens - ERRO ao salvar imagem '{imagem.FileName}' para Unidade ID:{unidade.Id}");
                }
            }
            else
            {
                _logger.LogInformation($"UnidadeController/SalvarImagens - Imagem ignorada pois seu tamanho é zero. Unidade ID:{unidade.Id}");
            }
        }

        await _context.SaveChangesAsync();
        _logger.LogInformation($"UnidadeController/SalvarImagens - Todas as imagens da Unidade ID:{unidade.Id} foram processadas e salvas com sucesso.");
    }

}