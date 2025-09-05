using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstate.Config;
using RealEstate.Models.Dtos;
using RealEstate.Models.Entities.Empreendimento;
using RealEstate.Services;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;


namespace RealEstate.Areas.Admin.Controllers;

[Area("Admin")]
public class EmpreendimentoController(ApplicationDbContext context, ILogger<EmpreendimentoController> logger)
    : Controller
{
    public async Task<IActionResult> Index()
    {
        try
        {
            var listaDeEmpreendimento = await context.Empreendimentos
                .Include(e => e.Endereco)
                .Include(e => e.imagens)
                .ToListAsync();

            List<EmpreendimentoViewModel> listaDeEmpreendimentoDto = new();

            foreach (var empreendimento in listaDeEmpreendimento)
            {
                EmpreendimentoViewModel dto = new()
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
                    Endereco = empreendimento.Endereco != null
                        ? new()
                        {
                            Bairro = empreendimento.Endereco.Bairro,
                            Cidade = empreendimento.Endereco.Cidade,
                            Cep = empreendimento.Endereco.Cep,
                            Complemento = empreendimento.Endereco.Complemento,
                            Uf = empreendimento.Endereco.Uf,
                            Logradouro = empreendimento.Endereco.Logradouro
                        } : null,
                    Imagens = empreendimento.imagens != null
                        ? empreendimento.imagens.Select(imagem => new ImagemViewModel
                        {
                            LargeCaminho = imagem.LargeCaminho,
                            ThumbCaminho = imagem.ThumbCaminho,
                            MediumCaminho = imagem.MediumCaminho,
                            XLargeCaminho = imagem.XLargeCaminho,
                        }).ToList()
                        : null
                };

                logger.LogInformation($"Empreendimento/Index chamado em {DateTime.Now}", DateTime.Now);
                listaDeEmpreendimentoDto.Add(dto);
            }

            return View(listaDeEmpreendimentoDto);
        }
        catch (Exception e)
        {
            logger.LogError($"Empreendimento/Index chamado em {DateTime.Now}", DateTime.Now,
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

        var empreendimento = await context.Empreendimentos
            .FirstOrDefaultAsync(m => m.Id == id);
        if (empreendimento == null)
        {
            return NotFound();
        }

        return View(empreendimento);
    }

    public IActionResult Create()
    {
        return View();
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

            Empreendimento empreendimento = new()
            {
                //AreaConstruidaMax = vm.AreaConstruidaMax,
                //AreaConstruidaMin = vm.AreaConstruidaMin,
                //BanheirosMax = vm.BanheirosMax,
                //BanheirosMin = vm.BanheirosMin,
                //Descricao = vm.Descricao,
                //DormitoriosMax = vm.DormitoriosMax,
                //DormitoriosMin = vm.DormitoriosMin,
                Nome = vm.Nome,
                Status = vm.Status,
                //SuitesMax = vm.SuitesMax,
                //SuitesMin = vm.SuitesMin,
                //VagasDeGaragemMax = vm.VagasDeGaragemMax,
                //VagasDeGaragemMin = vm.VagasDeGaragemMin,
                CriadoEm = DateTime.UtcNow,
                CriadoPor = Guid.NewGuid(),
                Ativo = true,
                AtualizadoEm = DateTime.UtcNow,
                AtualizadoPor = Guid.NewGuid(),

                Endereco = vm.Endereco != null
                    ? new()
                    {
                        Bairro = vm.Endereco.Bairro,
                        Cidade = vm.Endereco.Cidade,
                        Cep = vm.Endereco.Cep,
                        Complemento = vm.Endereco.Complemento,
                        Uf = vm.Endereco.Uf,
                        Logradouro = vm.Endereco.Logradouro,
                    }
                    : null,

                imagens = null,
            };

            context.Add(empreendimento);
            var teste = await context.SaveChangesAsync();
            ViewBag.Mensagem = "Empreendimento criado com sucesso!";

            await SalvarImagensAsync(vm.ArquivoImagens, empreendimento.Nome ?? string.Empty, empreendimento.Id, "Fachada");

            return RedirectToAction(nameof(Index));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var empreendimento = await context.Empreendimentos.FindAsync(id);
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
    public async Task<IActionResult> Edit(Guid id,
        [Bind(
            "Nome,Descricao,AreaConstruidaMin,AreaConstruidaMax,DormitoriosMin,DormitoriosMax,BanheirosMin,BanheirosMax,CriadoPor,CriadoEm,AtualizadoPor,AtualizadoEm,Ativo,Id")]
        Empreendimento empreendimento)
    {
        if (id != empreendimento.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                context.Update(empreendimento);
                await context.SaveChangesAsync();
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

    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var empreendimento = await context.Empreendimentos
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
        var empreendimento = await context.Empreendimentos.FindAsync(id);
        if (empreendimento != null)
        {
            context.Empreendimentos.Remove(empreendimento);
        }

        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool EmpreendimentoExists(Guid id)
    {
        return context.Empreendimentos.Any(e => e.Id == id);
    }

    private async Task<bool> SalvarImagensAsync(List<IFormFile> arquivos, string nomeEmpreendimento, Guid empreendimentoId, string tipoImagem)
    {
        if (arquivos == null || arquivos.Count == 0)
            throw new ArgumentException("Nenhuma imagem enviada.");

        // 1. Pasta de destino
        var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens");
        if (!Directory.Exists(uploadDir))
            Directory.CreateDirectory(uploadDir);

        // 2. Lista para armazenar dados temporários (arquivo + tamanho)
        var imagensTemp = new List<(int Area, string Caminho, string Extensao)>();

        // 3. Processar cada arquivo
        foreach (var arquivo in arquivos)
        {
            if (arquivo != null && arquivo.Length > 0)
            {
                using var imgStream = arquivo.OpenReadStream();
                using var image = await Image.LoadAsync(imgStream);
                int area = image.Width * image.Height;

                string extensao = Path.GetExtension(arquivo.FileName);
                string fileName = $"{tipoImagem}_{nomeEmpreendimento.Trim()}{extensao}";
                string filePath = Path.Combine(uploadDir, fileName);
                string caminho = "/imagens/" + fileName;

                // Salva arquivo no disco
                await using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await arquivo.CopyToAsync(stream);
                }

                imagensTemp.Add((area, caminho, extensao));
            }
        }

        if (imagensTemp.Count != 4)
            throw new InvalidOperationException("Devem ser enviadas exatamente 4 imagens.");

        // 4. Ordena do menor para o maior tamanho
        var ordenadas = imagensTemp.OrderBy(x => x.Area).ToList();

        // 5. Preenche objeto Imagem
        var imagem = new Imagem
        {
            ThumbCaminho = ordenadas[0].Caminho,
            MediumCaminho = ordenadas[1].Caminho,
            LargeCaminho = ordenadas[2].Caminho,
            XLargeCaminho = ordenadas[3].Caminho,
            
            ThumbTamanho = ordenadas[0].Area.ToString(),
            MediumTamanho = ordenadas[1].Area.ToString(),
            LargeTamanho = ordenadas[2].Area.ToString(),
            XLargeTamanho = ordenadas[3].Area.ToString(),
            
            ThumbExtensao = ordenadas[0].Area.ToString(),
            MediumExtensao = ordenadas[1].Area.ToString(),
            LargeExtensao = ordenadas[2].Area.ToString(),
            XLargeExtensao = ordenadas[3].Area.ToString(),
            
            
            EmpreendimentoId = empreendimentoId
        };

        context.Imagens.Add(imagem);
        await context.SaveChangesAsync();

        return true;
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