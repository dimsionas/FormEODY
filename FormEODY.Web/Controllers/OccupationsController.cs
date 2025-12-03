using FormEODY.DataAccess.Entities;
using FormEODY.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FormEODY.Web.Controllers;

[Authorize(Roles = "Editor")]
public class OccupationsController : Controller
{
    private readonly IOccupationService _occupationService;

    public OccupationsController(IOccupationService occupationService)
    {
        _occupationService = occupationService;
    }

    public async Task<IActionResult> Index()
    {
        var occupations = await _occupationService.GetAllAsync();
        return View(occupations);
    }

    [HttpPost]
    public async Task<IActionResult> Create(string name)
    {
        if (!string.IsNullOrWhiteSpace(name))
        {
            await _occupationService.AddAsync(new Occupation { Name = name.Trim() });
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        await _occupationService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
