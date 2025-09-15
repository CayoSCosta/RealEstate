using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RealEstate.Config;
using RealEstate.Models;
using RealEstate.Models.Entities.Empreendimento;
using RealEstate.Models.ViewModels;

namespace RealEstate.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UnidadeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UnidadeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Unidade
        public async Task<IActionResult> Index()
        {
            var unidades = await _context.Unidades
                .Include(u => u.Empreendimento) 
                .ToListAsync();

            List<UnidadeViewModel> list = new();
            if (unidades != null)
            {
                foreach (var unidade in unidades)
                {
                    UnidadeViewModel vm = new();
                    vm.Id = unidade.Id;
                    vm.Tipo = unidade.Tipo;
                    vm.Dormitorios = unidade.Dormitorios;
                    vm.Status = unidade.Status;
                    vm.Suites = unidade.Suites;
                    vm.Banheiros = unidade.Banheiros;
                    vm.VagasDeGaragem = unidade.Vagas;
                    vm.AreaConstruida = unidade.AreaConstruida;
                    vm.Valor = unidade.Valor ?? 0;
                    vm.EmpreendimentoId = unidade.EmpreendimentoId ?? Guid.Empty;
                    vm.Empreendimento = unidade.Empreendimento != null
                        ? new EmpreendimentoViewModel
                        {
                            Id = unidade.Empreendimento.Id,
                            Nome = unidade.Empreendimento.Nome
                        }
                        : null;
                    vm.Ativo = unidade.Ativo;
                    list.Add(vm);
                }
                
            }

            return View(list);
        }

        // GET: Admin/Unidade/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
                return NotFound();


            var unidade = await _context.Unidades
                .FirstOrDefaultAsync(m => m.Id == id);

            UnidadeViewModel vm = new();
            if (unidade != null)
            {
                vm.Id = unidade.Id;
                vm.Tipo = unidade.Tipo;
                vm.Dormitorios = unidade.Dormitorios;
                vm.Status = unidade.Status;
                vm.Suites = unidade.Suites;
                vm.Banheiros = unidade.Banheiros;
                vm.VagasDeGaragem = unidade.Vagas;
                vm.AreaConstruida = unidade.AreaConstruida;
                vm.Valor = unidade.Valor ?? 0;
                vm.EmpreendimentoId = unidade.EmpreendimentoId ?? Guid.Empty;
                vm.Ativo = unidade.Ativo;
            }       

            if (unidade == null)
                return NotFound();

            return View(vm);
        }

        // GET: Admin/Unidade/Create
        public IActionResult Create(Guid? empreendimentoId)
        {
            ViewBag.Empreendimentos = new SelectList(_context.Empreendimentos, "Id", "Nome");

            var vm = new UnidadeViewModel();

            if (empreendimentoId.HasValue)
                vm.EmpreendimentoId = empreendimentoId.Value;

            return View(vm);
        }

        // POST: Admin/Unidade/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UnidadeViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            Unidade unidade = new()
            {
                Tipo = vm.Tipo,
                Dormitorios = vm.Dormitorios,
                Suites = vm.Suites,
                Banheiros = vm.Banheiros,
                Vagas = vm.VagasDeGaragem,
                AreaConstruida = vm.AreaConstruida,
                Status = vm.Status,
                Valor = vm.Valor,
                EmpreendimentoId = vm.EmpreendimentoId,
                Imagens = new List<Imagem>()
            };

            // Salvar unidade primeiro
            _context.Unidades.Add(unidade);
            await _context.SaveChangesAsync();

            // Salvar as imagens (se houver upload)
            if (vm.Plantas != null && vm.Plantas.Any())
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/plantas");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                foreach (var file in vm.Plantas)
                {
                    if (file.Length > 0)
                    {
                        var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                        var filePath = Path.Combine(uploadsFolder, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        var imagem = new Imagem
                        {
                            Tipo = TipoImagem.Plantas,
                            LargeCaminho = "/uploads/plantas/" + fileName,
                            LargeExtensao = Path.GetExtension(file.FileName),
                            UnidadeId = unidade.Id
                        };

                        _context.Imagens.Add(imagem);
                    }
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }


        // GET: Admin/Unidade/Edit/5
        public IActionResult Edit(Guid id)
        {
            var unidade = _context.Unidades
                .Include(u => u.Empreendimento)
                .Include(u => u.Imagens)
                .FirstOrDefault(u => u.Id == id);

            if (unidade == null)
                return NotFound();

            var vm = new UnidadeViewModel
            {
                Id = unidade.Id,
                Ativo = unidade.Ativo,
                Tipo = unidade.Tipo,
                Dormitorios = unidade.Dormitorios,
                Suites = unidade.Suites,
                Banheiros = unidade.Banheiros,
                VagasDeGaragem = unidade.Vagas,
                AreaConstruida = unidade.AreaConstruida,
                EmpreendimentoId = unidade.EmpreendimentoId ?? Guid.Empty,
                Empreendimento = unidade.Empreendimento != null
                    ? new EmpreendimentoViewModel
                    {
                        Id = unidade.Empreendimento.Id,
                        Nome = unidade.Empreendimento.Nome
                    }
                    : null,
                Imagens = unidade.Imagens != null
                    ? unidade.Imagens.Select(i => new ImagemViewModel
                    {
                        Id = i.Id,
                        ThumbCaminho = i.ThumbCaminho,
                        LargeCaminho = i.LargeCaminho,
                        MediumCaminho = i.MediumCaminho,
                        XLargeCaminho = i.XLargeCaminho,
                        EmpreendimentoId = i.EmpreendimentoId,
                        //UnidadeId = i.UnidadeId,
                        Empreendimento = null, //TODO: Rever essa propriedade: Empreendimento
                        Unidade = null, //TODO: Rever essa propriedade: Unidade
                        ThumbExtensao = i.ThumbExtensao,
                        MediumExtensao = i.MediumExtensao,
                        LargeExtensao = i.LargeExtensao,
                        XLargeExtensao = i.XLargeExtensao,
                        ThumbTamanho = i.ThumbTamanho,
                        MediumTamanho = i.MediumTamanho,
                        XLargeTamanho = i.XLargeTamanho,
                        LargeTamanho = i.LargeTamanho,                    
                        Tipo = i.Tipo
                    }).ToList()
                    : new List<ImagemViewModel>(),
            };

            ViewBag.Empreendimentos = new SelectList(
                _context.Empreendimentos, "Id", "Nome", unidade.EmpreendimentoId
            );

            return View(vm);
        }

        // POST: Admin/Unidade/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UnidadeViewModel vm)
        {
            var unidade = await _context.Unidades
                .Include(u => u.Empreendimento)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (id != unidade.Id)
                return NotFound();

            unidade.Id = vm.Id;
            unidade.Tipo = vm.Tipo;
            unidade.Dormitorios = vm.Dormitorios;
            unidade.Suites = vm.Suites;
            unidade.Banheiros = vm.Banheiros;
            unidade.Vagas = vm.VagasDeGaragem;
            unidade.AreaConstruida = vm.AreaConstruida;
            unidade.Status = vm.Status;
            unidade.Valor = vm.Valor;
            unidade.EmpreendimentoId = vm.EmpreendimentoId;
            unidade.Ativo = vm.Ativo;
            

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
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            return View(vm);
        }

        // GET: Admin/Unidade/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unidade = await _context.Unidades
                .FirstOrDefaultAsync(m => m.Id == id);

            if (unidade == null)
            {
                return NotFound();
            }

            UnidadeViewModel vm = new();
            vm.Id = unidade.Id;
            vm.Tipo = unidade.Tipo;
            vm.Dormitorios = unidade.Dormitorios;
            vm.Status = unidade.Status;
            vm.Suites = unidade.Suites;
            vm.Banheiros = unidade.Banheiros;
            vm.VagasDeGaragem = unidade.Vagas;
            vm.AreaConstruida = unidade.AreaConstruida;
            vm.Valor = unidade.Valor ?? 0;
            vm.EmpreendimentoId = unidade.EmpreendimentoId ?? Guid.Empty;
            vm.Ativo = unidade.Ativo;


            return View(vm);
        }

        // POST: Admin/Unidade/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var unidade = await _context.Unidades.FindAsync(id);
            if (unidade != null)
            {
                _context.Unidades.Remove(unidade);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UnidadeExists(Guid id)
        {
            return _context.Unidades.Any(e => e.Id == id);
        }
    }
}
