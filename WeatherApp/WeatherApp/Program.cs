using WeatherApp;
using WeatherApp.Translations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
                .AddMvcLocalizations();

builder.Logging.AddLog4Net();

builder.Services.AddServicesLocalizations();

builder.Services.AddWeatherApp();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
// Blazor
// else
//{
//    app.UseWebAssemblyDebugging();
//}

//app.UseBlazorFrameworkFiles();
//app.MapFallbackToFile("index.html");
// Blazor

app.AddWepApplicationLocalizations("fr-FR", "en-US");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
