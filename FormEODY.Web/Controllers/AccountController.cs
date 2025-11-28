using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using FormEODY.Web.Models;

namespace FormEODY.Web.Controllers;

public class AccountController : Controller
{
    private readonly SignInManager<IdentityUser> _signInManager;

    public AccountController(SignInManager<IdentityUser> signInManager)
    {
        _signInManager = signInManager;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

        if (result.Succeeded)
            return Redirect("/Applications");

        ViewBag.Error = "Invalid credentials";
        return View(model);
    }
}
