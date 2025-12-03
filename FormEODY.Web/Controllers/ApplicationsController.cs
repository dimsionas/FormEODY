using FormEODY.DataAccess.Entities;
using FormEODY.Services.Interfaces;
using FormEODY.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FormEODY.Web.Controllers;
[Authorize]
public class ApplicationsController : Controller
{
    private readonly IApplicationService _applicationService;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<ApplicationsController> _logger;
    private readonly IOccupationService _occupationService;

    public ApplicationsController(
        IApplicationService applicationService,
        IMapper mapper,
        UserManager<ApplicationUser> userManager,
        ILogger<ApplicationsController> logger,
        IOccupationService occupationService)
    {
        _applicationService = applicationService;
        _mapper = mapper;
        _userManager = userManager;
        _logger = logger;
        _occupationService = occupationService;
    }

    [Authorize]
    public async Task<IActionResult> Index()
    {
        var apps = await _applicationService.GetAllAsync();
        var vm = _mapper.Map<List<ApplicationViewModel>>(apps);
        return View(vm);
    }

    [Authorize(Roles = "Editor")]
    public async Task<IActionResult> Create()
    {
        await PopulateOccupationsAsync();
        return View(new ApplicationViewModel());
    }

    [HttpPost]
    [Authorize(Roles = "Editor")]
    public async Task<IActionResult> Create(ApplicationViewModel model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                await PopulateOccupationsAsync();
                return View(model);
            }

            var entity = _mapper.Map<Application>(model);
            var userId = _userManager.GetUserId(User);
            entity.CreatedBy = userId;
            entity.CreatedAt = DateTime.UtcNow;

            await _applicationService.AddAsync(entity);
            TempData["Success"] = "Application submitted successfully!";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating application");
            ModelState.AddModelError(string.Empty, "An error occurred while creating the application.");
            await PopulateOccupationsAsync();
            return View(model);
        }
    }

    [Authorize(Roles = "Editor")]
    public async Task<IActionResult> Edit(int id)
    {
        var app = await _applicationService.GetByIdAsync(id);
        if (app == null) return NotFound();
        var vm = _mapper.Map<ApplicationViewModel>(app);
        await PopulateOccupationsAsync();
        return View(vm);
    }

    [Authorize(Roles = "Editor")]
    [HttpPost]
    public async Task<IActionResult> Edit(ApplicationViewModel model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                await PopulateOccupationsAsync();
                return View(model);
            }

            var existing = await _applicationService.GetByIdAsync(model.Id);
            if (existing == null) return NotFound();

            _mapper.Map(model, existing);
            existing.UpdatedBy = _userManager.GetUserId(User);
            existing.UpdatedAt = DateTime.UtcNow;

            await _applicationService.UpdateAsync(existing);
            TempData["Success"] = "Application updated successfully!";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating application");
            ModelState.AddModelError(string.Empty, "An error occurred while updating the application.");
            await PopulateOccupationsAsync();
            return View(model);
        }
    }

    [Authorize]
    public async Task<IActionResult> Details(int id)
    {
        var app = await _applicationService.GetByIdAsync(id);
        if (app == null) return NotFound();
        var vm = _mapper.Map<ApplicationViewModel>(app);
        return View(vm);
    }

    [HttpGet]
    public IActionResult Data(DataSourceLoadOptions loadOptions)
    {
        var query = _applicationService.Query()
            .Select(a => new
            {
                id = a.Id,
                name = a.Name,
                gender = (int)a.Gender,
                single = a.Single,
                occupationName = a.Occupation != null ? a.Occupation.Name : string.Empty,
                message = a.Message
            });
        return Json(DataSourceLoader.Load(query, loadOptions));
    }

    [Authorize(Roles = "Editor")]
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        await _applicationService.DeleteAsync(id);
        TempData["Success"] = "Application deleted.";
        return RedirectToAction(nameof(Index));
    }

    private async Task PopulateOccupationsAsync()
    {
        var occupations = await _occupationService.GetAllAsync();
        ViewBag.Occupations = occupations
            .Select(o => new SelectListItem { Value = o.Id.ToString(), Text = o.Name })
            .ToList();
    }
}
