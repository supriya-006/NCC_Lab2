using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApp12BySupriya.Data;

namespace WebApp12BySupriya.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AdminController(
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    // Admin dashboard
    public IActionResult Index()
    {
        return View();
    }

    // =========================
    // USERS
    // =========================

    public IActionResult Users()
    {
        var users = _userManager.Users.ToList();

        return View(users);
    }

    // Create user
    [HttpPost]
    public async Task<IActionResult> CreateUser(
        string email,
        string password)
    {
        var user = new IdentityUser
        {
            UserName = email,
            Email = email,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(
            user,
            password);

        if (result.Succeeded)
        {
            TempData["Message"] = "User created successfully.";
        }
        else
        {
            TempData["Message"] =
                string.Join(", ",
                result.Errors.Select(e => e.Description));
        }

        return RedirectToAction(nameof(Users));
    }

    // Delete user
    [HttpPost]
    public async Task<IActionResult> DeleteUser(
        string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        if (user != null)
        {
            await _userManager.DeleteAsync(user);
        }

        return RedirectToAction(nameof(Users));
    }

    // =========================
    // ROLES
    // =========================

    public IActionResult Roles()
    {
        var roles = _roleManager.Roles.ToList();

        return View(roles);
    }

    // Create role
    [HttpPost]
    public async Task<IActionResult> CreateRole(
        string roleName)
    {
        if (!string.IsNullOrWhiteSpace(roleName))
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                await _roleManager.CreateAsync(
                    new IdentityRole(roleName));
            }
        }

        return RedirectToAction(nameof(Roles));
    }

    // Delete role
    [HttpPost]
    public async Task<IActionResult> DeleteRole(
        string roleId)
    {
        var role = await _roleManager.FindByIdAsync(roleId);

        if (role != null && role.Name != "Admin")
        {
            await _roleManager.DeleteAsync(role);
        }

        return RedirectToAction(nameof(Roles));
    }

    // =========================
    // MANAGE USER
    // =========================

    public async Task<IActionResult> ManageUser(
        string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        ViewBag.User = user;

        ViewBag.Roles =
            await _userManager.GetRolesAsync(user);

        ViewBag.Claims =
            await _userManager.GetClaimsAsync(user);

        ViewBag.AllRoles =
            _roleManager.Roles.ToList();

        return View();
    }

    // Assign role
    [HttpPost]
    public async Task<IActionResult> AddRole(
        string userId,
        string roleName)
    {
        var user =
            await _userManager.FindByIdAsync(userId);

        if (user != null &&
            await _roleManager.RoleExistsAsync(roleName))
        {
            await _userManager.AddToRoleAsync(
                user,
                roleName);
        }

        return RedirectToAction(
            nameof(ManageUser),
            new { id = userId });
    }

    // Revoke role
    [HttpPost]
    public async Task<IActionResult> RemoveRole(
        string userId,
        string roleName)
    {
        var user =
            await _userManager.FindByIdAsync(userId);

        if (user != null)
        {
            await _userManager.RemoveFromRoleAsync(
                user,
                roleName);
        }

        return RedirectToAction(
            nameof(ManageUser),
            new { id = userId });
    }

    // Add claim
    [HttpPost]
    public async Task<IActionResult> AddClaim(
        string userId,
        string claimType,
        string claimValue)
    {
        var user =
            await _userManager.FindByIdAsync(userId);

        if (user != null)
        {
            await _userManager.AddClaimAsync(
                user,
                new Claim(
                    claimType,
                    claimValue));
        }

        return RedirectToAction(
            nameof(ManageUser),
            new { id = userId });
    }

    // Remove claim
    [HttpPost]
    public async Task<IActionResult> RemoveClaim(
        string userId,
        string claimType,
        string claimValue)
    {
        var user =
            await _userManager.FindByIdAsync(userId);

        if (user != null)
        {
            await _userManager.RemoveClaimAsync(
                user,
                new Claim(
                    claimType,
                    claimValue));
        }

        return RedirectToAction(
            nameof(ManageUser),
            new { id = userId });
    }
}