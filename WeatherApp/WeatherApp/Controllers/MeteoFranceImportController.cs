using Microsoft.AspNetCore.Mvc;
using WeatherApp.Services;

namespace WeatherApp.Controllers
{
	public class MeteoFranceImportController : Controller
	{
		private readonly IMeteoFranceImportDataService meteoFranceImportDataService;

		public MeteoFranceImportController(IMeteoFranceImportDataService meteoFranceImportDataService)
		{
			this.meteoFranceImportDataService = meteoFranceImportDataService;
		}

		// GET: MeteoFranceImportController
		public async Task<ActionResult> Index()
		{
			await this.meteoFranceImportDataService.ImportAsync();
			return View();
		}
	}
}
