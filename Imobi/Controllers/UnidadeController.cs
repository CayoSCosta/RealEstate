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
                    await AtualizarCaracteristicasDeUnidadesAsync(unidade.EmpreendimentoId);
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

        private async Task<Unidade?> ObterUnidade(int? id)
        {
            return await _context.Unidades
                .Include(e => e.Arquivos)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        private async Task<Empreendimento> AtualizarCaracteristicasDeUnidadesAsync(int empreendimentoId)
        {
            try
            {
                var emp = await _context.Empreendimentos
                                .Include(e => e.Unidades)
                                .FirstOrDefaultAsync(m => m.Id == empreendimentoId);

                //var emp = await _context.Empreendimentos
                //    .Include(e => e.Unidades)
                //    .FirstOrDefaultAsync(e => e.Id == unidade.EmpreendimentoId);

                if (emp == null)
                    return emp;

                if (emp == null || emp.Unidades == null || !emp.Unidades.Any())
                {
                    if (emp != null)
                    {
                        emp.AreaConstruida = "0 m²";
                        emp.BanheirosTotal = "0";
                        emp.DormitoriosTotal = "0";
                        emp.SuitesTotal = "0";
                        emp.VagasTotal = "0";
                        _context.Update(emp);
                        await _context.SaveChangesAsync();
                    }

                    return emp;
                }

                var areaConstruidaMin = emp.Unidades.Min(u => u.AreaConstruida);
                var areaConstruidaMax = emp.Unidades.Max(u => u.AreaConstruida);

                var banheirosMin = emp.Unidades.Min(u => u.Banheiros);
                var banheirosMax = emp.Unidades.Max(u => u.Banheiros);

                var dormitoriosMin = emp.Unidades.Min(u => u.Dormitorios);
                var dormitoriosMax = emp.Unidades.Max(u => u.Dormitorios);

                var suitesMin = emp.Unidades.Min(u => u.Suites);
                var suitesMax = emp.Unidades.Max(u => u.Suites);

                var vagasMin = emp.Unidades.Min(u => u.Vagas);
                var vagasMax = emp.Unidades.Max(u => u.Vagas);

                emp.BanheirosTotal = banheirosMin == banheirosMax ? $"{banheirosMin}" : $"{banheirosMin} - {banheirosMax}";
                emp.DormitoriosTotal = dormitoriosMin == dormitoriosMax ? $"{dormitoriosMin}" : $"{dormitoriosMin} - {dormitoriosMax}";
                emp.SuitesTotal = suitesMin == suitesMax ? $"{suitesMin}" : $"{suitesMin} - {suitesMax}";
                emp.VagasTotal = vagasMin == vagasMax ? $"{vagasMin}" : $"{vagasMin} - {vagasMax}";
                emp.AreaConstruida = areaConstruidaMin == areaConstruidaMax ? $"{areaConstruidaMin} m²" : $"{areaConstruidaMin} - {areaConstruidaMax} m²";

                _context.Update(emp);
                _context.SaveChanges();
                return emp;
            }
            catch (Exception)
            {
                throw;
            }
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