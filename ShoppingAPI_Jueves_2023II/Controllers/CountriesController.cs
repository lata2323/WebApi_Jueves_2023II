using Microsoft.AspNetCore.Mvc;
using ShoppingAPI_Jueves_2023II.DAL__DataAccessLayer_.Entities;
using ShoppingAPI_Jueves_2023II.Domain.Interfaces;
using System.Xml.Linq;

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

        //Aqui comienza uno de los endpoint
        [HttpGet, ActionName("Get")]
        [Route("GetById/{id}")] //Aqui concateno la URL inicial: URL = api/countries/get/{id}
        public async Task<ActionResult<IEnumerable<Country>>> GetCountryByIdAsync(Guid id)
        {
            if (id == null)
            {
                return BadRequest("Id es requerido!");
            }

            var country = await _countryService.GetCountryByIdAsync(id); //Aqui se esta llendo a la capa de domain para traerme la lista de paises

            if (country == null) return NotFound(); //NotFound = 404 HTTP Status code

            return Ok(country); //Ok = 200 HTTP Status Code
        }
        //aqui finaliza el endpoint

        [HttpGet, ActionName("Get")]
        [Route("GetByName/{name}")] //Aqui concateno la URL inicial: URL = api/countries/get/{id}
        public async Task<ActionResult<IEnumerable<Country>>> GetCountryByNameAsync(string name)
        {
            if (name == null) return BadRequest("Nombre es requerido!");

            var country = await _countryService.GetCountryByNameAsync(name); //Aqui se esta llendo a la capa de domain para traerme la lista de paises

            if (country == null) return NotFound(); //NotFound = 404 HTTP Status code

            return Ok(country); //Ok = 200 HTTP Status Code
        }

        [HttpPut, ActionName("Edit")] //HTTP Put es para modificar, actualizar lo que ya esta en la BD
        [Route("Edit")]
        public async Task<ActionResult> EditCountryAsync(Country country)
        {
            try
            {
                var editedCountry = await _countryService.EditCountryAsync(country);

                return Ok(editedCountry); //Retorne un 200 y el objeto Country
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpDelete, ActionName("Delete")] //HTTP Delete es para eliminar, actualizar lo que ya esta en la BD
        [Route("Delete")]
        public async Task<ActionResult> DeleteCountryAsync(Guid id)
        {
                if (id == null) return BadRequest("Id es requerido!");

                var deleteCountry = await _countryService.DeleteCountryAsync(id);

                if (deleteCountry == null) return NotFound("País no encontrado!");

                return Ok(deleteCountry); //Retorne un 200 y el objeto Country
        }
    }
}
