using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountriesController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Country>>> GetAll()
        {
            var countries = await _countryService.GetAllCountriesAsync();

            return Ok(countries);
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<CountryDetails>> Get(string name)
        {
            var country = await _countryService.GetCountryDetailsAsync(name);

            return Ok(country);
        }
    }
}
