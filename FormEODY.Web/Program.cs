using FormEODY.DataAccess;
using Microsoft.EntityFrameworkCore;
using FormEODY.Services.Interfaces;
using FormEODY.Services.Implementations;
using FormEODY.Web.Data;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddScoped<IApplicationService, ApplicationService>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Cookie settings
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.ReturnUrlParameter = "returnUrl";
});

var app = builder.Build();

// Seed roles & users
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
    await IdentitySeed.InitializeAsync(
        services.GetRequiredService<UserManager<IdentityUser>>(),
        services.GetRequiredService<RoleManager<IdentityRole>>()
    );
}

// Redirect root (/) to /Applications
app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/Applications");
        return;
    }

    await next();
});

// Middleware pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

// IMPORTANT middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

// Default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Applications}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages();
app.Urls.Add("http://0.0.0.0:6001");
app.Run();
