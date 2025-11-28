using FormEODY.DataAccess.Entities;
using FormEODY.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace FormEODY.Web.Controllers;
[Authorize]
public class ApplicationsController : Controller
{
    private readonly IApplicationService _applicationService;

    public ApplicationsController(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    [Authorize]
    public async Task<IActionResult> Index()
    {
        var apps = await _applicationService.GetAllAsync();
        return View(apps);
    }

    [Authorize(Roles = "Editor")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [Authorize(Roles = "Editor")]
    public async Task<IActionResult> Create(Application model)
    {
        if (!ModelState.IsValid)
            return View(model);

        await _applicationService.AddAsync(model);
        TempData["Success"] = "Application submitted successfully!";
        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = "Editor")]
    public async Task<IActionResult> Edit(int id)
    {
        var app = await _applicationService.GetByIdAsync(id);
        if (app == null) return NotFound();
        return View(app);
    }

    [Authorize(Roles = "Editor")]
    [HttpPost]
    public async Task<IActionResult> Edit(Application model)
    {
        if (!ModelState.IsValid)
            return View(model);

        await _applicationService.UpdateAsync(model);
        TempData["Success"] = "Application updated successfully!";
        return RedirectToAction(nameof(Index));
    }

    [Authorize]
    public async Task<IActionResult> Details(int id)
    {
        var app = await _applicationService.GetByIdAsync(id);
        if (app == null) return NotFound();
        return View(app);
    }

    [Authorize(Roles = "Editor")]
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        await _applicationService.DeleteAsync(id);
        TempData["Success"] = "Application deleted.";
        return RedirectToAction(nameof(Index));
    }
}
