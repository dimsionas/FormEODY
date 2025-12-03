using Microsoft.AspNetCore.Identity;
using FormEODY.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FormEODY.Web.Controllers;

public class LogoutController : Controller
{
    private readonly SignInManager<ApplicationUser> _signInManager;

    public LogoutController(SignInManager<ApplicationUser> signInManager)
    {
        _signInManager = signInManager;
    }

    [HttpPost]
    public async Task<IActionResult> Index()
    {
        await _signInManager.SignOutAsync();
        return Redirect("/Account/Login");  // or "/Applications"
    }
}
