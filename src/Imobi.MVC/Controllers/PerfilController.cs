using Imobi.Data.Identity;
using Imobi.MVC.Models.ViewModels.MeuPerfil;
using Imobi.MVC.ViewModels; // Certifique-se que sua ViewModel está aqui
// Se estiver em outra pasta, mantenha o using do seu snippet:
// using Imobi.MVC.Models.ViewModels.MeuPerfil; 
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Imobi.Controllers
{
    [Authorize]
    public class PerfilController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<PerfilController> _logger;

        public PerfilController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IWebHostEnvironment env,
            ILogger<PerfilController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _env = env;
            _logger = logger;
        }

        // GET: /Perfil
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            ApplicationUser? user = null;

            if (!string.IsNullOrEmpty(userId))
                user = await _userManager.FindByIdAsync(userId);

            if (user == null && User.Identity?.Name != null)
                user = await _userManager.FindByEmailAsync(User.Identity.Name);

            if (user == null) return NotFound("Usuário não encontrado.");

            var vm = new MeuPerfilViewModel
            {
                Nome = user.Nome,
                SobreNome = user.SobreNome,
                Email = user.Email!,
                EmailSecundario = user.EmailSecundario,
                FotoAtual = user.FotoUrl
            };

            return View(vm);
        }

        // POST: /Perfil
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(MeuPerfilViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var userId = _userManager.GetUserId(User);
            ApplicationUser? user = null;

            if (!string.IsNullOrEmpty(userId))
            {
                user = await _userManager.FindByIdAsync(userId);
            }

            if (user == null && User.Identity?.Name != null)
            {
                user = await _userManager.FindByEmailAsync(User.Identity.Name);
            }

            if (user == null)
            {
                return NotFound("Usuário não encontrado. Tente fazer login novamente.");
            }

            user.Nome = model.Nome;
            user.SobreNome = model.SobreNome;
            user.EmailSecundario = model.EmailSecundario;

            if (model.Email != user.Email)
            {
                user.Email = model.Email;
                user.UserName = model.Email;
            }

            if (!string.IsNullOrEmpty(model.FotoBase64))
            {
                user.FotoUrl = model.FotoBase64;
                user.ModificadoEm = DateTime.UtcNow;
            }

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);
            }

            await _signInManager.RefreshSignInAsync(user);

            TempData["Sucesso"] = "Perfil atualizado com sucesso!";

            return RedirectToAction(nameof(Index));
        }

        // POST: /Perfil/AlterarSenha
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AlterarSenha(MeuPerfilViewModel model)
        {

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            if (string.IsNullOrEmpty(model.SenhaAtual) || string.IsNullOrEmpty(model.NovaSenha))
            {
                TempData["Erro"] = "Preencha os campos de senha.";
                return RedirectToAction(nameof(Index));
            }

            var result = await _userManager.ChangePasswordAsync(
                user,
                model.SenhaAtual,
                model.NovaSenha);

            if (!result.Succeeded)
            {
                var erros = string.Join(", ", result.Errors.Select(e => e.Description));
                TempData["Erro"] = $"Erro ao alterar senha: {erros}";
                return RedirectToAction(nameof(Index));
            }

            await _signInManager.RefreshSignInAsync(user);
            TempData["Sucesso"] = "Senha alterada com sucesso.";

            return RedirectToAction(nameof(Index));
        }
    }
}