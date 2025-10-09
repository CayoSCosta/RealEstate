using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstate.Config;
using RealEstate.Models;
using RealEstate.Models.Entities.Empreendimento;
using RealEstate.Models.ViewModels;
using RealEstate.Services;
using SixLabors.ImageSharp;
using System.Data;


namespace RealEstate.Areas.Admin.Controllers;

[Area("Admin")]
public class EmpreendimentoController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<EmpreendimentoController> _logger;
    private readonly IImagemService _imagemService;

    public EmpreendimentoController(ApplicationDbContext context, ILogger<EmpreendimentoController> logger, IImagemService imagemService)
    {
        _context = context;
        _logger = logger;
        _imagemService = imagemService;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var listaDeEmpreendimento = await _context.Empreendimentos
                .Include(e => e.Endereco)
                .Include(e => e.Imagens)
                .Include(e => e.Unidades)
                .ToListAsync();

            List<EmpreendimentoViewModel> listaDeEmpreendimentoDto = new();

            foreach (var empreendimento in listaDeEmpreendimento)
            {
                EmpreendimentoViewModel vm = new()
                {
                    Id = empreendimento.Id,
                    Nome = empreendimento.Nome,
                    Descricao = empreendimento.Descricao,
                    AreaConstruidaMin = empreendimento.AreaConstruidaMin,
                    AreaConstruidaMax = empreendimento.AreaConstruidaMax,
                    DormitoriosMin = empreendimento.DormitoriosMin,
                    DormitoriosMax = empreendimento.DormitoriosMax,
                    BanheirosMin = empreendimento.BanheirosMin,
                    BanheirosMax = empreendimento.BanheirosMax,
                    SuitesMin = empreendimento.SuitesMin,
                    SuitesMax = empreendimento.SuitesMax,
                    VagasDeGaragemMin = empreendimento.VagasDeGaragemMin,
                    VagasDeGaragemMax = empreendimento.VagasDeGaragemMax,
                    Status = empreendimento.Status,
                    Ativo = empreendimento.Ativo,

                    Unidades = empreendimento.Unidades != null
                        ? empreendimento.Unidades.Select(u => new UnidadeViewModel
                        {
                            Id = u.Id,
                            Tipo = u.Tipo,
                            Dormitorios = u.Dormitorios,
                            Suites = u.Suites,
                            Banheiros = u.Banheiros,
                            VagasDeGaragem = u.Vagas,
                            AreaConstruida = u.AreaConstruida,
                            Valor = u.Valor ?? 0,
                            Status = u.Status,
                            EmpreendimentoId = u.EmpreendimentoId ?? Guid.Empty
                        }).ToList()
                        : null,

                    Endereco = empreendimento.Endereco != null
                        ? new()
                        {
                            Bairro = empreendimento.Endereco?.Bairro,
                            Cidade = empreendimento.Endereco?.Cidade,
                            Cep = empreendimento.Endereco?.Cep,
                            Complemento = empreendimento.Endereco?.Complemento,
                            Uf = empreendimento.Endereco?.Uf,
                            Logradouro = empreendimento.Endereco?.Logradouro
                        } : null,
                };

                _logger.LogInformation($"Empreendimento/Index chamado em {DateTime.Now}", DateTime.Now);
                listaDeEmpreendimentoDto.Add(vm);
            }

            return View(listaDeEmpreendimentoDto);
        }
        catch (Exception e)
        {
            _logger.LogError($"Empreendimento/Index chamado em {DateTime.Now}", DateTime.Now,
                $"Erro: {e.Message}, \n InnerException: {e.InnerException?.Message}");
            throw;
        }
    }

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

    public IActionResult Create()
    {
        var vm = new EmpreendimentoViewModel();
        return View(vm);
    }

    // POST: Admin/Empreendimento/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(EmpreendimentoViewModel vm)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Mensagens = ModelState;
                return View(vm);
            }

            Empreendimento empreendimento = new();
            empreendimento.Nome = vm.Nome?.Trim();
            empreendimento.Status = vm.Status;
            empreendimento.Descricao = vm.Descricao;

            if (vm.Endereco != null)
            {
                empreendimento.Endereco = new Endereco
                {
                    Logradouro = vm.Endereco.Logradouro,
                    Bairro = vm.Endereco.Bairro,
                    Cidade = vm.Endereco.Cidade,
                    Cep = vm.Endereco.Cep,
                    Numero = vm.Endereco.Numero,
                    Complemento = vm.Endereco.Complemento,
                    Uf = vm.Endereco.Uf,
                };
            }
            else
                empreendimento.Endereco = null;

            await _context.Empreendimentos.AddAsync(empreendimento);

            if (vm.Arquivos != null)
            {
                foreach (var arquivo in vm.Arquivos)
                {
                    var imagem = await _imagemService.ProcessarImagemAsync(arquivo, empreendimento.Nome, TipoArquivo.Imagens, TipoEntidade.Empreendimento, empreendimento.Id);
                    await _context.Imagens.AddAsync(imagem);
                }

            }

            await _context.SaveChangesAsync();

            ViewBag.Mensagem = "Empreendimento criado com sucesso!";

            return RedirectToAction(nameof(Index));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var empreendimento = await _context.Empreendimentos
            .Include(e => e.Unidades)
            .Include(e => e.Endereco)
            .Include(e => e.Imagens)
            .Include(e => e.Arquivos)
            .FirstOrDefaultAsync(e => e.Id == id);

        EmpreendimentoViewModel vm = new();
        if (empreendimento != null)
        {
            vm.Status = empreendimento.Status;
            vm.Nome = empreendimento.Nome;
            vm.Descricao = empreendimento.Descricao;
            vm.AreaConstruidaMin = empreendimento.AreaConstruidaMin;
            vm.AreaConstruidaMax = empreendimento.AreaConstruidaMax;
            vm.DormitoriosMin = empreendimento.DormitoriosMin;
            vm.DormitoriosMax = empreendimento.DormitoriosMax;
            vm.BanheirosMin = empreendimento.BanheirosMin;
            vm.BanheirosMax = empreendimento.BanheirosMax;
            vm.SuitesMin = empreendimento.SuitesMin;
            vm.SuitesMax = empreendimento.SuitesMax;
            vm.VagasDeGaragemMin = empreendimento.VagasDeGaragemMin;
            vm.VagasDeGaragemMax = empreendimento.VagasDeGaragemMax;
            vm.Id = empreendimento.Id;
            vm.Endereco = new EnderecoViewModel
            {
                Bairro = empreendimento.Endereco?.Bairro,
                Cidade = empreendimento.Endereco?.Cidade,
                Cep = empreendimento.Endereco?.Cep,
                Complemento = empreendimento.Endereco?.Complemento,
                Uf = empreendimento.Endereco?.Uf,
                Logradouro = empreendimento.Endereco?.Logradouro
            };
            vm.Unidades = empreendimento.Unidades != null
                ? empreendimento.Unidades.Select(u => new UnidadeViewModel
                {
                    Id = u.Id,
                    Tipo = u.Tipo,
                    Dormitorios = u.Dormitorios,
                    Suites = u.Suites,
                    Banheiros = u.Banheiros,
                    VagasDeGaragem = u.Vagas,
                    AreaConstruida = u.AreaConstruida,
                    Valor = u.Valor ?? 0,
                    Status = u.Status,
                    EmpreendimentoId = u.EmpreendimentoId ?? Guid.Empty
                }).ToList()
                : null;

            vm.Imagens = empreendimento.Arquivos?.Select(i => new ArquivoViewModel
                {
                    NomeArquivo = i.NomeArquivo,
                    Caminho = i.Caminho,
                }).ToList();

            if (empreendimento == null)
                return NotFound();
        }

        return View(vm);
    }

    // POST: Admin/Empreendimento/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, EmpreendimentoViewModel empreendimentoVm)
    {
        if (id != empreendimentoVm.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(empreendimentoVm);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpreendimentoExists(empreendimentoVm.Id))
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

        return View(empreendimentoVm);
    }

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

    [HttpGet]
    public async Task<IActionResult> BuscarCep(string cep, [FromServices] ViaCepService viaCepService)
    {
        var resultado = await viaCepService.BuscarEnderecoPorCep(cep);
        if (resultado is null)
            return NotFound("CEP não encontrado");

        return Json(resultado);
    }
}