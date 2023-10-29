using Microsoft.AspNetCore.Mvc;
using ShoppingAPI_Jueves_2023II.DAL__DataAccessLayer_.Entities;
using ShoppingAPI_Jueves_2023II.Domain.Interfaces;
using ShoppingAPI_Jueves_2023II.Domain.Services;

namespace ShoppingAPI_Jueves_2023II.Controllers
{
    [ApiController]
    [Route("api/[controller]")] //Esta es la primera parte de la url de esta api: URL = api/states
    public class StatesController : Controller
    {
        private readonly IStateService _stateService;

        public StatesController(IStateService stateService)
        {
            _stateService = stateService;
        }

        [HttpGet, ActionName("Get")]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<State>>> GetStatesByCountryIdAsync(Guid countryId)
        {
            var states = await _stateService.GetStatesByCountryIdAsync(countryId); 

            if (states == null || !states.Any()) return NotFound();

            return Ok(states); //Ok = 200 HTTP Status Code
        }

        [HttpPost, ActionName("Create")]
        [Route("Create")]
        public async Task<ActionResult<State>> CreateStateAsync(State state, Guid countryId)
        {
            try
            {
                var createdState = await _stateService.CreateStateAsync(state, countryId);

                if (createdState == null) return NotFound();

                return Ok(createdState); //Retorne un 200 y el objeto Country
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate"))
                {
                    return Conflict(String.Format("El país {0} ya existe", state.Name));
                }
                return Conflict(ex.Message);
            }
        }

        [HttpGet, ActionName("Get")]
        [Route("GetById/{id}")] //Aqui concateno la URL inicial: URL = api/countries/get/{id}
        public async Task<ActionResult<IEnumerable<Country>>> GetStateByIdAsync(Guid id)
        {
            if (id == null) return BadRequest("Id es requerido!");

            var state = await _stateService.GetStateByIdAsync(id); //Aqui se esta llendo a la capa de domain para traerme la lista de paises

            if (state == null) return NotFound(); //NotFound = 404 HTTP Status code

            return Ok(state); //Ok = 200 HTTP Status Code
        }

        [HttpPut, ActionName("Edit")] //HTTP Put es para modificar, actualizar lo que ya esta en la BD
        [Route("Edit")]
        public async Task<ActionResult<State>> EditStateAsync(State state, Guid id)
        {
            try
            {
                var editedState = await _stateService.EditStateAsync(state, id);

                return Ok(editedState); //Retorne un 200 y el objeto Country
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpDelete, ActionName("Delete")] //HTTP Delete es para eliminar, actualizar lo que ya esta en la BD
        [Route("Delete")]
        public async Task<ActionResult<State>> DeleteStateAsync(Guid id)
        {
            if (id == null) return BadRequest("Id es requerido!");

            var deleteCountry = await _stateService.DeleteStateAsync(id);

            if (deleteCountry == null) return NotFound("Estado no encontrado!");

            return Ok(deleteCountry); //Retorne un 200 y el objeto Country
        }
    }
}
