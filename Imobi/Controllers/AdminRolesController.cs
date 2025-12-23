using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Imobi.Models.ViewModels.AdminRoles;

namespace Imobi.Controllers;

[Authorize]
public class AdminRolesController : Controller
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ILogger<AdminRolesController> _logger;

    public AdminRolesController(RoleManager<IdentityRole> roleManager,
                                ILogger<AdminRolesController> logger)
    {
        _roleManager = roleManager;
        _logger = logger;
    }

    // LISTAGEM
    public async Task<IActionResult> Index()
    {
        var roles = await _roleManager.Roles.ToListAsync();
        return View(roles);
    }

    // CREATE GET
    public IActionResult Create() => View();

    // CREATE POST
    [HttpPost]
    public async Task<IActionResult> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            ModelState.AddModelError("", "O nome da role é obrigatório.");
            return View();
        }

        var result = await _roleManager.CreateAsync(new IdentityRole(name));

        if (!result.Succeeded)
        {
            foreach (var err in result.Errors)
                ModelState.AddModelError("", err.Description);

            return View();
        }

        return RedirectToAction(nameof(Index));
    }

    // EDIT GET
    public async Task<IActionResult> Edit(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);
        if (role == null) return NotFound();

        return View(role);
    }

    // EDIT POST
    [HttpPost]
    public async Task<IActionResult> Edit(string id, string name)
    {
        var role = await _roleManager.FindByIdAsync(id);
        if (role == null) return NotFound();

        role.Name = name;
        await _roleManager.UpdateAsync(role);

        return RedirectToAction(nameof(Index));
    }

    // DELETE
    public async Task<IActionResult> Delete(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);
        if (role == null) return NotFound();

        await _roleManager.DeleteAsync(role);
        return RedirectToAction(nameof(Index));
    }

    // CLAIMS DA ROLE - GET
    public async Task<IActionResult> Claims(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);
        if (role == null) return NotFound();

        var claims = await _roleManager.GetClaimsAsync(role);

        var model = new RoleClaimsViewModel
        {
            RoleId = role.Id,
            RoleName = role.Name,
            Claims = claims.Select(c => c.Type).ToList()
        };

        return View(model);
    }

    // ADICIONAR CLAIM
    [HttpPost]
    public async Task<IActionResult> AddClaim(RoleClaimsViewModel model)
    {
        var role = await _roleManager.FindByIdAsync(model.RoleId);
        if (role == null) return NotFound();

        await _roleManager.AddClaimAsync(role, new Claim(model.NewClaim, "true"));
        return RedirectToAction(nameof(Claims), new { id = model.RoleId });
    }

    // REMOVER CLAIM
    public async Task<IActionResult> RemoveClaim(string roleId, string claim)
    {
        var role = await _roleManager.FindByIdAsync(roleId);
        if (role == null) return NotFound();

        await _roleManager.RemoveClaimAsync(role, new Claim(claim, "true"));
        return RedirectToAction(nameof(Claims), new { id = roleId });
    }
}
