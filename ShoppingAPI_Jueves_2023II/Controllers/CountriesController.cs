using Microsoft.AspNetCore.Mvc;
using ShoppingAPI_Jueves_2023II.DAL__DataAccessLayer_.Entities;
using ShoppingAPI_Jueves_2023II.Domain.Interfaces;

namespace ShoppingAPI_Jueves_2023II.Controllers
{
    [ApiController]
    [Route("api/[controller]")] //Esta es la primera parte de la url de esta api: URL = api/countries
    public class CountriesController : Controller
    {

        private readonly ICountryService _countryService;

        public CountriesController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        //En un controlador los métodos cambian de nombre y realmente se llaman Acciones (Actions) - Si es una api se denomina endpoint
        //Todo endpoint retorna un ActionResult, significa que retorna el resultado de una acción.

        [HttpGet, ActionName("Get")]
        [Route("Get")] //Aqui concateno la URL inicial: URL = api/countries/get
        public async Task<ActionResult<IEnumerable<Country>>> GetCountriesAsync()
        {
            var countries = await _countryService.GetCountriesAsync(); //Aqui se esta llendo a la capa de domain para traerme la lista de paises

            if (countries == null || !countries.Any()) //El metodo any significa que hay almenos un elemento, pero como se niega es si está vacío
            {
                return NotFound(); //NotFound = 404 HTTP Status code
            }

            return Ok(countries); //Ok = 200 HTTP Status Code
        }

        [HttpPost, ActionName("Create")]
        [Route("Create")]
        public async Task<ActionResult> CreateCountryAsync(Country country)
        {
            try
            {
                var createdCountry = await _countryService.CreateCountryAsync(country);

                return Ok(createdCountry); //Retorne un 200 y el objeto Country
            }
            catch(Exception ex)
            {
                if (ex.Message.Contains("duplicate"))
                {
                    return Conflict(String.Format("El país {0} ya existe", country.Name));
                }
                return Conflict(ex.Message);
            }
        }
    }
}
