using global::Imobi.Models.Identity;
using global::Imobi.MVC.Models.ViewModels.MeuPerfil;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Imobi.MVC.Controllers;

[Authorize]
public class PerfilController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<PerfilController> _logger;

    public PerfilController(
        UserManager<ApplicationUser> userManager,
        IWebHostEnvironment env,
        ILogger<PerfilController> logger)
    {
        _userManager = userManager;
        _env = env;
        _logger = logger;
    }

    // GET: /Perfil
    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return NotFound();

        var vm = new MeuPerfilViewModel
        {
            Nome = user.Nome,
            SobreNome = user.SobreNome,
            Email = user.Email!,
            EmailSecundario = user.EmailSecundario,
            FotoUrl = user.FotoUrl
        };

        return View(vm);
    }

    // POST: /Perfil
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(MeuPerfilViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = await _userManager.GetUserAsync(User);
        if (user == null) return NotFound();

        user.Nome = model.Nome;
        user.SobreNome = model.SobreNome;
        user.Email = model.Email;
        user.UserName = model.Email; // importante
        user.EmailSecundario = model.EmailSecundario;

        // Upload da foto
        if (model.Foto != null)
        {
            var pasta = Path.Combine(_env.WebRootPath, "uploads", "perfil");
            Directory.CreateDirectory(pasta);

            var fileName = $"{user.Id}{Path.GetExtension(model.Foto.FileName)}";
            var path = Path.Combine(pasta, fileName);

            using var stream = new FileStream(path, FileMode.Create);
            await model.Foto.CopyToAsync(stream);

            user.FotoUrl = $"/uploads/perfil/{fileName}";
        }

        await _userManager.UpdateAsync(user);

        TempData["Sucesso"] = "Perfil atualizado com sucesso.";
        return RedirectToAction(nameof(Index));
    }

    // POST: /Perfil/AlterarSenha
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AlterarSenha(AlterarSenhaViewModel model)
    {
        if (!ModelState.IsValid)
            return RedirectToAction(nameof(Index));

        var user = await _userManager.GetUserAsync(User);
        if (user == null) return NotFound();

        var result = await _userManager.ChangePasswordAsync(
            user,
            model.SenhaAtual,
            model.NovaSenha);

        if (!result.Succeeded)
        {
            TempData["Erro"] = "Senha atual inválida ou nova senha não atende aos requisitos.";
            return RedirectToAction(nameof(Index));
        }

        TempData["Sucesso"] = "Senha alterada com sucesso.";
        return RedirectToAction(nameof(Index));
    }
}

