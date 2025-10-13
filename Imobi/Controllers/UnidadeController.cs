using Imobi.Config;
using Imobi.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Imobi.Controllers
{
    public class UnidadeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UnidadeController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Unidade
        public async Task<IActionResult> Index()
        {
            var unidades = _context.Unidades.Include(u => u.Empreendimento);
            return View(unidades);
        }

        // GET: Unidade/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unidade = await _context.Unidades
                .Include(u => u.Empreendimento)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (unidade == null)
            {
                return NotFound();
            }

            return View(unidade);
        }

        // GET: Unidade/Create
        public IActionResult Create()
        {
            ViewData["EmpreendimentoId"] = new SelectList(_context.Empreendimentos, "Id", "Id");
            return View();
        }

        // POST: Unidade/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Unidade unidade)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(unidade);
                    await _context.SaveChangesAsync();
                    await SalvarImagens(unidade);
                    return RedirectToAction(nameof(Index));
                }
                ViewData["EmpreendimentoId"] = new SelectList(_context.Empreendimentos, "Id", "Id", unidade.EmpreendimentoId);
                return View(unidade);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task SalvarImagens(Unidade unidade)
        {
            if (unidade.Imagens != null && unidade.Imagens.Any())
            {
                foreach (var imagem in unidade.Imagens)
                {
                    if (imagem.Length > 0)
                    {

                        string empreendimentoId = unidade.EmpreendimentoId.ToString();
                        string unidadeId = unidade.Id.ToString();

                        //verifica pasta empreendimento
                        var caminhoPastaEmpreendimento = Path.Combine(_webHostEnvironment.WebRootPath, "Imagens", "Empreendimentos", empreendimentoId);
                        if (!Directory.Exists(caminhoPastaEmpreendimento))
                            Directory.CreateDirectory(caminhoPastaEmpreendimento);

                        //verifica pasta unidade
                        var caminhoPastaUnidade = Path.Combine(caminhoPastaEmpreendimento, unidadeId);
                        if (!Directory.Exists(caminhoPastaUnidade))
                            Directory.CreateDirectory(caminhoPastaUnidade);

                        var nomeArquivo = Path.GetFileNameWithoutExtension(imagem.FileName);
                        var extensao = Path.GetExtension(imagem.FileName);
                        var nomeFinal = $"{Guid.NewGuid()}{extensao}";
                        var caminhoArquivo = Path.Combine(caminhoPastaUnidade, nomeFinal);

                        using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
                            await imagem.CopyToAsync(stream);

                        var caminhoRelativo = Path.Combine("Imagens", "Empreendimentos", empreendimentoId, unidadeId, nomeFinal).Replace("\\", "/");

                        Arquivo arquivo = new()
                        {
                            NomeArquivo = nomeArquivo,
                            Extensao = extensao,
                            Tipo = TipoArquivo.Imagem,
                            Caminho = caminhoRelativo,
                            EmpreendimentoId = unidade.Id
                        };

                        _context.Arquivos.Add(arquivo);
                    }
                }

                await _context.SaveChangesAsync();
            }
        }

        // GET: Unidade/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unidade = await _context.Unidades.FindAsync(id);
            if (unidade == null)
            {
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Status,Tipo,Dormitorios,Suites,Banheiros,Vagas,AreaConstruida,Valor,EmpreendimentoId")] Unidade unidade)
        {
            if (id != unidade.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(unidade);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UnidadeExists(unidade.Id))
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
            ViewData["EmpreendimentoId"] = new SelectList(_context.Empreendimentos, "Id", "Id", unidade.EmpreendimentoId);
            return View(unidade);
        }

        // GET: Unidade/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unidade = await _context.Unidades
                .Include(u => u.Empreendimento)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (unidade == null)
            {
                return NotFound();
            }

            return View(unidade);
        }

        // POST: Unidade/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var unidade = await _context.Unidades.FindAsync(id);
            if (unidade != null)
            {
                _context.Unidades.Remove(unidade);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UnidadeExists(int id)
        {
            return _context.Unidades.Any(e => e.Id == id);
        }
    }
}
