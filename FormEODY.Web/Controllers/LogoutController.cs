using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FormEODY.Web.Controllers;

public class LogoutController : Controller
{
    private readonly SignInManager<IdentityUser> _signInManager;

    public LogoutController(SignInManager<IdentityUser> signInManager)
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
