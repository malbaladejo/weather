using Microsoft.AspNetCore.Mvc;
using WeatherApp.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WeatherApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeteoFranceApiController : Controller
    {
        private readonly IDepartmentService departmentService;
        private readonly IMeteoFranceApiService meteoFranceApiService;
        private readonly ILogger<MeteoFranceApiController> logger;

        public MeteoFranceApiController(
            IDepartmentService departmentService,
            IMeteoFranceApiService meteoFranceApiService,
            ILogger<MeteoFranceApiController> logger)
        {
            this.departmentService = departmentService;
            this.meteoFranceApiService = meteoFranceApiService;
            this.logger = logger;
        }

        // GET: api/<WeatherController>
        [HttpGet("departments")]
        public IEnumerable<Department> Get()
        {
            this.logger.LogInformation("/api/MeteoFranceApi/departments");
            return this.departmentService.GetDepartments();
        }

        // GET: api/<WeatherController>
        [HttpGet("departments/{code}")]
        public Task<IReadOnlyCollection<Station>> Get(string code)
        {
            this.logger.LogInformation($"/api/MeteoFranceApi/departments/code{code}");
            return this.meteoFranceApiService.GetStationsAsync(code);
        }
    }
}
