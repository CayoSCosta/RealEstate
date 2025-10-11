using Imobi.Config;
using Imobi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Imobi.Controllers;

public class ArquivoController : Controller
{
    private readonly ApplicationDbContext _context;

    public ArquivoController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Arquivos
    public async Task<IActionResult> Index()
    {
        return View(await _context.Arquivos.ToListAsync());
    }

    // GET: Arquivos/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var arquivo = await _context.Arquivos
            .FirstOrDefaultAsync(m => m.Id == id);
        if (arquivo == null)
        {
            return NotFound();
        }

        return View(arquivo);
    }

    // GET: Arquivos/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Arquivos/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,NomeArquivo,Caminho,Extensao,Tipo,EmpreendimentoId")] Arquivo arquivo)
    {
        if (ModelState.IsValid)
        {
            _context.Add(arquivo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(arquivo);
    }

    // GET: Arquivos/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var arquivo = await _context.Arquivos.FindAsync(id);
        if (arquivo == null)
        {
            return NotFound();
        }
        return View(arquivo);
    }

    // POST: Arquivos/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,NomeArquivo,Caminho,Extensao,Tipo,EmpreendimentoId")] Arquivo arquivo)
    {
        if (id != arquivo.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(arquivo);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArquivoExists(arquivo.Id))
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
        return View(arquivo);
    }

    // GET: Arquivos/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var arquivo = await _context.Arquivos
            .FirstOrDefaultAsync(m => m.Id == id);
        if (arquivo == null)
        {
            return NotFound();
        }

        return View(arquivo);
    }

    // POST: Arquivos/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var arquivo = await _context.Arquivos.FindAsync(id);
        if (arquivo != null)
        {
            _context.Arquivos.Remove(arquivo);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ArquivoExists(int id)
    {
        return _context.Arquivos.Any(e => e.Id == id);
    }
}
