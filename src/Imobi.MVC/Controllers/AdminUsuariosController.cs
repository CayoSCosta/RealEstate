using Imobi.Models.Identity;
using Imobi.Models.ViewModels.AdminUsuarios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Imobi.Controllers;

[Authorize]
public class AdminUsuariosController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ILogger<AdminUsuariosController> _logger;

    public AdminUsuariosController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ILogger<AdminUsuariosController> logger)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _logger = logger;
    }

    // LISTA DE USUÁRIOS
    public async Task<IActionResult> Index()
    {
        var users = await _userManager.Users.ToListAsync();

        var model = new List<UsuarioListItemViewModel>();

        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);

            model.Add(new UsuarioListItemViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Ativo = user.Ativo,
                RolesNames = roles.ToList()
            });
        }

        return View(model);
    }

    // DETALHES
    public async Task<IActionResult> Details(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        var vm = new UsuarioListItemViewModel();
        if (user != null)
        {
            var roles = await _userManager.GetRolesAsync(user);
            vm.Email = user.Email;
            vm.Ativo = user.Ativo;
            vm.Id = user.Id;
            vm.Nome = user.Nome;
            vm.SobreNome = user.SobreNome;
            vm.RolesNames = roles.ToList();
        }

        if (user == null)
        {
            _logger.LogInformation("AdminUsuarioController/Details - user == null");
            return NotFound();
        }

        return View(vm);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return NotFound();

        var userRoles = await _userManager.GetRolesAsync(user);
        var allRoles = _roleManager.Roles.Select(r => r.Name).ToList();

        var vm = new UsuarioListItemViewModel
        {
            Id = user.Id,
            Nome = user.Nome,
            SobreNome = user.SobreNome,
            Email = user.Email,
            Ativo = user.Ativo,
            RolesNames = userRoles.ToList(),
            Roles = allRoles.Select(r => new RoleSelection
            {
                Name = r,
                Selected = userRoles.Contains(r)
            }).ToList()
        };

        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(UsuarioListItemViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = await _userManager.FindByIdAsync(model.Id);
        if (user == null) return NotFound();

        user.Nome = model.Nome;
        user.SobreNome = model.SobreNome;
        user.Email = model.Email;
        user.Ativo = model.Ativo;

        await _userManager.UpdateAsync(user);

        // Atualiza roles
        var rolesAtuais = await _userManager.GetRolesAsync(user);
        var rolesSelecionadas = model.Roles.Where(r => r.Selected).Select(r => r.Name);

        await _userManager.RemoveFromRolesAsync(user, rolesAtuais);
        await _userManager.AddToRolesAsync(user, rolesSelecionadas);

        TempData["Sucesso"] = "Usuário alterado com sucesso";

        return RedirectToAction(nameof(Edit), new { id = user.Id });
    }

    // ATIVAR / DESATIVAR
    public async Task<IActionResult> ToggleStatus(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return NotFound();

        user.Ativo = !user.Ativo;
        await _userManager.UpdateAsync(user);

        return RedirectToAction(nameof(Index));
    }

    // RESETAR SENHA (gera senha nova)
    public async Task<IActionResult> ResetPassword(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return NotFound();

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var novaSenha = "SenhaTemp123!";

        var result = await _userManager.ResetPasswordAsync(user, token, novaSenha);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        TempData["Sucesso"] = $"Senha do usuário {user.Nome} redefinida para: {novaSenha}";
        return RedirectToAction(nameof(Index));
    }

    // EDITAR ROLES
    public async Task<IActionResult> EditRoles(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return NotFound();

        var allRoles = _roleManager.Roles.Select(r => r.Name).ToList();
        var userRoles = await _userManager.GetRolesAsync(user);

        var model = new EditRolesViewModel
        {
            UserId = user.Id,
            Email = user.Email,
            Roles = allRoles.Select(role => new RoleSelection
            {
                Name = role,
                Selected = userRoles.Contains(role)
            }).ToList()
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> EditRoles(EditRolesViewModel model)
    {
        var user = await _userManager.FindByIdAsync(model.UserId);
        if (user == null) return NotFound();

        var current = await _userManager.GetRolesAsync(user);

        await _userManager.RemoveFromRolesAsync(user, current);

        var selected = model.Roles.Where(r => r.Selected).Select(r => r.Name);
        await _userManager.AddToRolesAsync(user, selected);

        return RedirectToAction(nameof(Index));
    }
}
