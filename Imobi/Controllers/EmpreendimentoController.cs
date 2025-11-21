using Imobi.Config;
using Imobi.Models;
using Imobi.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Imobi.Controllers;

public class EmpreendimentoController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public EmpreendimentoController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
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
            return NotFound();

        Empreendimento? empreendimento = await ObterEmpreendimento(id);

        if (empreendimento == null)
            return NotFound();

        return View(empreendimento);
    }

    // GET: Empreendimento/Create
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
        try
        {
            if (ModelState.IsValid)
            {
                _context.Add(empreendimento);
                await _context.SaveChangesAsync();
                await SalvarImagens(empreendimento);
                return RedirectToAction(nameof(Index));
            }

            return View(empreendimento);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "Erro ao criar empreendimento: " + ex.Message);
            return View(empreendimento);
        }
    }

    private async Task SalvarImagens(Empreendimento empreendimento)
    {
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
                            Directory.CreateDirectory(caminhoPasta);

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
                            Descricao = "Teste descrição de imagem empreendimento",
                        };

                        _context.Arquivos.Add(arquivo);
                    }
                }

                await _context.SaveChangesAsync();
            }
        }
        catch (Exception)
        {

            throw;
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
    public async Task<IActionResult> Edit(int id, Empreendimento empreendimento)
    {
        if (id != empreendimento.Id)
            return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                var original = _context.Empreendimentos.Find(id);
                if (original != null)
                {                   
                    if(original.SuitesTotal != empreendimento.SuitesTotal || original.DormitoriosTotal !=empreendimento.DormitoriosTotal || 
                        original.BanheirosTotal != empreendimento.BanheirosTotal || original.VagasTotal != empreendimento.VagasTotal)
                    {
                        return BadRequest("Tentativa de alteração não permitida.");
                    }
                }
                else
                {
                    _context.Update(empreendimento);
                    await SalvarImagens(empreendimento);
                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpreendimentoExists(empreendimento.Id))
                    return NotFound();
                else
                    throw;
            }
            return RedirectToAction(nameof(Index));
        }

        return View(empreendimento);
    }

    // GET: Empreendimento/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var empreendimento = await ObterEmpreendimento(id);

        if (empreendimento == null)
            return NotFound();

        return View(empreendimento);
    }

    // POST: Empreendimento/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var empreendimento = await ObterEmpreendimento(id);
        if (empreendimento != null)
            _context.Empreendimentos.Remove(empreendimento);

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool EmpreendimentoExists(int id)
    {
        return _context.Empreendimentos.Any(e => e.Id == id);
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

    [HttpPost]
    public async Task<IActionResult> RemoverImagem(int id)
    {
        var imagem = await _context.Arquivos
            .FirstOrDefaultAsync(a => a.Id == id && a.EmpreendimentoId != null);

        if (imagem == null)
            return NotFound();

        try
        {
            // Apagar o arquivo físico
            var caminhoFisico = Path.Combine(_webHostEnvironment.WebRootPath, imagem.Caminho.Replace("/", Path.DirectorySeparatorChar.ToString()));
            if (System.IO.File.Exists(caminhoFisico))
                System.IO.File.Delete(caminhoFisico);

            _context.Arquivos.Remove(imagem);
            await _context.SaveChangesAsync();

            return Ok();
        }
        catch (Exception)
        {
            return StatusCode(500, "Erro ao remover imagem do empreendimento.");
        }
    }
}
