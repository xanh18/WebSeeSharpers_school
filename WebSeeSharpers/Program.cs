using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebSeeSharpers.Data;
using WebSeeSharpers.Models;
using SimpleInjector;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<WebSeeSharpersContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WebSeeSharpersContext")));


//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
//      .AddEntityFrameworkStores<WebSeeSharpersContext>();
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<WebSeeSharpersContext>().AddDefaultUI();

// Add services to the container.
builder.Services.AddControllersWithViews();

//support globalization and localization
builder.Services.AddLocalization(option => { option.ResourcesPath = "Resources"; });
builder.Services.AddMvc().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix).AddDataAnnotationsLocalization();
builder.Services.Configure<RequestLocalizationOptions>(
    opt =>
    {
        var supportedCultures = new List<CultureInfo>
        {
            new CultureInfo("nl"),
            new CultureInfo("en")
        };
        opt.DefaultRequestCulture = new RequestCulture("nl");
        opt.SupportedCultures = supportedCultures;
        opt.SupportedUICultures = supportedCultures;
    });


builder.Services.AddAuthentication()
    .AddGoogle(options => {
    options.ClientId = "408553945001-bqp41jloevqsq59b7tk0qlhuouqu4o5q.apps.googleusercontent.com";
    options.ClientSecret = "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";
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

app.UseRequestLocalization(((IApplicationBuilder) app).ApplicationServices
    .GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();
