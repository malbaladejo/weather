using WeatherApp;
using WeatherApp.Translations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
                .AddMvcLocalizations();

//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new Info { Title = "Meteo France API", Version = "v1" });
//});

builder.Logging.AddLog4Net();

builder.Services.AddServicesLocalizations();

builder.Services.AddWeatherApp();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

}
else
{
    app.UseWebAssemblyDebugging();
}

app.UseBlazorFrameworkFiles();
app.MapFallbackToFile("index.html");

app.AddWepApplicationLocalizations("fr-FR", "en-US");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
