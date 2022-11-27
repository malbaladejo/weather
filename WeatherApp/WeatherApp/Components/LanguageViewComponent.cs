using Microsoft.AspNetCore.Mvc;

namespace WeatherApp.Components
{
    [ViewComponent]
    public class LanguageViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(new LanguageViewModel(this.Request.Path.Value, this.Request.Query));
        }
    }
}
