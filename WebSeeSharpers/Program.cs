using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using WebSeeSharpers.Data;
using WebSeeSharpers.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<WebSeeSharpersContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WebSeeSharpersContext")));

// Add services to the container.
builder.Services.AddControllersWithViews();

//support globalization and localization
builder.Services.AddLocalization(option => { option.ResourcesPath = "Resources"; });
builder.Services.AddMvc().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix).AddDataAnnotationsLocalization();

builder.Services.Configure<RequestLocalizationOptions>(
    options =>
    {
        var supportedCulteres = new List<CultureInfo>
        {
            new CultureInfo("en"),
            new CultureInfo("nl")
        };
        options.DefaultRequestCulture = new RequestCulture("nl");
        options.SupportedCultures = supportedCulteres;
        options.SupportedUICultures = supportedCulteres;
    });

//add servies to the container
builder.Services.AddControllersWithViews();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

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

app.UseAuthorization();

var options = ((IApplicationBuilder)app).ApplicationServices.GetRequiredService<IOptions<RequestLocalizationOptions>>();

app.UseRequestLocalization(options.Value);

//var supportedCultures = new[] { "en", "nl" };
//var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[1])
//    .AddSupportedCultures(supportedCultures)
//    .AddSupportedUICultures(supportedCultures);

//app.UseRequestLocalization(localizationOptions);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
