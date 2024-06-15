using Microsoft.AspNetCore.Mvc;

namespace WeatherApp.Controllers
{
    public class MeteoFranceController : Controller
    {
        // GET: MeteoFranceController
        public ActionResult Index()
        {
            return View();
        }
    }
}
