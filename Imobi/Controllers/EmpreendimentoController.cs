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

    public EmpreendimentoController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Empreendimento
    public async Task<IActionResult> Index()
    {
        var applicationDbContext = _context.Empreendimentos.Include(e => e.Endereco);
        return View(await applicationDbContext.ToListAsync());
    }

    // GET: Empreendimento/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var empreendimento = await _context.Empreendimentos
            .Include(e => e.Endereco)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (empreendimento == null)
        {
            return NotFound();
        }

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
        if (ModelState.IsValid)
        {
            _context.Add(empreendimento);

            Arquivo arquivo = new();
            arquivo.NomeArquivo = "imagem-empreendimento";
            arquivo.Extensao = ".jpg";
            arquivo.Tipo = TipoArquivo.Imagem;
            arquivo.Caminho = Path.Combine("uploads", "empreendimentos", empreendimento.Nome.ToLower().Replace(" ", "-"), arquivo.NomeArquivo + arquivo.Extensao);

            _context.Arquivos.Add(arquivo);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(empreendimento);
    }

    // GET: Empreendimento/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var empreendimento = await _context.Empreendimentos.FindAsync(id);
        if (empreendimento == null)
        {
            return NotFound();
        }
        ViewData["EnderecoId"] = new SelectList(_context.Enderecos, "Id", "Id", empreendimento.EnderecoId);
        return View(empreendimento);
    }

    // POST: Empreendimento/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Status,Nome,Sobre,AreaConstruida,Estagio,BanheirosTotal,DormitoriosTotal,SuitesTotal,VagasTotal,UnidadeId,EnderecoId")] Empreendimento empreendimento)
    {
        if (id != empreendimento.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(empreendimento);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpreendimentoExists(empreendimento.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["EnderecoId"] = new SelectList(_context.Enderecos, "Id", "Id", empreendimento.EnderecoId);
        return View(empreendimento);
    }

    // GET: Empreendimento/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var empreendimento = await _context.Empreendimentos
            .Include(e => e.Endereco)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (empreendimento == null)
        {
            return NotFound();
        }

        return View(empreendimento);
    }

    // POST: Empreendimento/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var empreendimento = await _context.Empreendimentos.FindAsync(id);
        if (empreendimento != null)
        {
            _context.Empreendimentos.Remove(empreendimento);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool EmpreendimentoExists(int id)
    {
        return _context.Empreendimentos.Any(e => e.Id == id);
    }

    [HttpGet]
    public async Task<IActionResult> BuscarCep(string cep, [FromServices] ViaCepService viaCepService)
    {
        var resultado = await viaCepService.BuscarEnderecoPorCep(cep);
        if (resultado is null)
            return NotFound("CEP não encontrado");

        return Json(resultado);
    }
}
