using Imobi.Config;
using Imobi.Models;
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
        public IActionResult Index()
        {
            var unidades = _context.Unidades.Include(u => u.Empreendimento);
            return View(unidades);
        }

        // GET: Unidade/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var unidade = await _context.Unidades
                .Include(u => u.Empreendimento)
                .Include(u => u.Arquivos)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (unidade == null)
                return NotFound();

            return View(unidade);
        }

        // GET: Unidade/Create
        public IActionResult Create()
        {
            ViewData["EmpreendimentoId"] = new SelectList(_context.Empreendimentos, "Id", "Nome");
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
                    await AtualizarCaracteristicasDeUnidadesAsync(unidade.EmpreendimentoId);
                    TempData["Sucesso"] = $"Unidade CRIADA com sucesso!";
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
                            Descricao = "Teste descrição de imagem unidade",
                            EmpreendimentoId = int.Parse(empreendimentoId),
                            UnidadeId = int.Parse(unidadeId),
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
                return NotFound();

            var unidade = await _context.Unidades
                .Include(u => u.Arquivos)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (unidade == null)
                return NotFound();

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
            if (id != unidade.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                //foreach (var entry in ModelState)
                //{
                //    var key = entry.Key;
                //    var errors = entry.Value.Errors;
                //    foreach (var error in errors)
                //    {
                //        Console.WriteLine($"Erro no campo {key}: {error.ErrorMessage}");
                //    }
                //}

                try
                {
                    _context.Update(unidade);
                    await AtualizarCaracteristicasDeUnidadesAsync(unidade.EmpreendimentoId);
                    await SalvarImagens(unidade);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UnidadeExists(unidade.Id))
                        return NotFound();
                    else
                        throw;
                }
                TempData["Sucesso"] = $"Unidade ATUALIZADA com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmpreendimentoId"] = new SelectList(_context.Empreendimentos, "Id", "Id", unidade.EmpreendimentoId);
            return View(unidade);
        }

        // GET: Unidade/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var unidade = await _context.Unidades
                .Include(u => u.Empreendimento)
                .Include(u => u.Arquivos)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (unidade == null)
                return NotFound();

            return View(unidade);
        }

        // POST: Unidade/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var unidade = await ObterUnidade(id);

            if (unidade != null)
                _context.Unidades.Remove(unidade);

            await _context.SaveChangesAsync();
            TempData["Sucesso"] = $"Unidade DELETADA com sucesso!";
            return RedirectToAction(nameof(Index));
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
                return null;

            var unidades = emp.Unidades;

            // Se não tem unidades, zera tudo
            if (unidades == null || !unidades.Any())
            {
                emp.AreaConstruida = "0 m²";
                emp.BanheirosTotal = "0";
                emp.DormitoriosTotal = "0";
                emp.SuitesTotal = "0";
                emp.VagasTotal = "0";

                await _context.SaveChangesAsync();
                return emp;
            }

            // Função auxiliar para ranges
            static string Range(int min, int max) =>
                min == max ? $"{min}" : $"{min} - {max}";

            static string RangeArea(int min, int max) =>
                min == max ? $"{min} m²" : $"{min} - {max} m²";

            emp.BanheirosTotal = Range(unidades.Min(u => u.Banheiros), unidades.Max(u => u.Banheiros));
            emp.DormitoriosTotal = Range(unidades.Min(u => u.Dormitorios), unidades.Max(u => u.Dormitorios));
            emp.SuitesTotal = Range(unidades.Min(u => u.Suites), unidades.Max(u => u.Suites));
            emp.VagasTotal = Range(unidades.Min(u => u.Vagas), unidades.Max(u => u.Vagas));

            emp.AreaConstruida = RangeArea(unidades.Min(u => u.AreaConstruida),unidades.Max(u => u.AreaConstruida)
            );

            await _context.SaveChangesAsync();

            return emp;
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
}