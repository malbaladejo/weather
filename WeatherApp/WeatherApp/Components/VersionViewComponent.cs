using Microsoft.AspNetCore.Mvc;

namespace WeatherApp.Components
{
    [ViewComponent]
    public class VersionViewComponent : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            var version = this.GetType().Assembly.GetName().Version;
            return View(new { Version = $"v{version.Major}.{version.Minor}.{version.Build}" });
        }
    }
}
