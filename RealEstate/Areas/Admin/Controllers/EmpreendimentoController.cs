using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstate.Config;
using RealEstate.Models.Entities.Empreendimento;

namespace RealEstate.Areas.Admin.Controllers;

[Area("Admin")]
public class EmpreendimentoController : Controller
{
    private readonly ApplicationDbContext _context;

    public EmpreendimentoController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Admin/Empreendimento
    public async Task<IActionResult> Index()
    {
        return View(await _context.Empreendimentos.ToListAsync());
    }

    // GET: Admin/Empreendimento/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var empreendimento = await _context.Empreendimentos
            .FirstOrDefaultAsync(m => m.Id == id);
        if (empreendimento == null)
        {
            return NotFound();
        }

        return View(empreendimento);
    }

    // GET: Admin/Empreendimento/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Admin/Empreendimento/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Nome,Descricao,AreaConstruidaMin,AreaConstruidaMax,DormitoriosMin,DormitoriosMax,BanheirosMin,BanheirosMax,CriadoPor,CriadoEm,AtualizadoPor,AtualizadoEm,Ativo,Id")] Empreendimento empreendimento)
    {
        if (ModelState.IsValid)
        {
            empreendimento.Id = Guid.NewGuid();
            _context.Add(empreendimento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(empreendimento);
    }

    // GET: Admin/Empreendimento/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
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
        return View(empreendimento);
    }

    // POST: Admin/Empreendimento/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Nome,Descricao,AreaConstruidaMin,AreaConstruidaMax,DormitoriosMin,DormitoriosMax,BanheirosMin,BanheirosMax,CriadoPor,CriadoEm,AtualizadoPor,AtualizadoEm,Ativo,Id")] Empreendimento empreendimento)
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
        return View(empreendimento);
    }

    // GET: Admin/Empreendimento/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var empreendimento = await _context.Empreendimentos
            .FirstOrDefaultAsync(m => m.Id == id);
        if (empreendimento == null)
        {
            return NotFound();
        }

        return View(empreendimento);
    }

    // POST: Admin/Empreendimento/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var empreendimento = await _context.Empreendimentos.FindAsync(id);
        if (empreendimento != null)
        {
            _context.Empreendimentos.Remove(empreendimento);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool EmpreendimentoExists(Guid id)
    {
        return _context.Empreendimentos.Any(e => e.Id == id);
    }
}
