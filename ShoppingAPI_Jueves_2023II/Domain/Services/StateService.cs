using Microsoft.EntityFrameworkCore;
using ShoppingAPI_Jueves_2023II.DAL__DataAccessLayer_;
using ShoppingAPI_Jueves_2023II.DAL__DataAccessLayer_.Entities;
using ShoppingAPI_Jueves_2023II.Domain.Interfaces;
using System.Diagnostics.Metrics;
using System.Xml.Linq;

namespace ShoppingAPI_Jueves_2023II.Domain.Services
{
    public class StateService : IStateService
    {
        public readonly DataBaseContext _context;

        public StateService(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<State>> GetStatesByCountryIdAsync(Guid countryId) //ICollection sirve para extraer los datos de forma más dinámica
        {
            return await _context.States
                .Where(s => s.CountryId == countryId)
                .ToListAsync();
        }

        public async Task<State> CreateStateAsync(State state, Guid countryId)
        {
            try
            {
                state.Id = Guid.NewGuid(); //Asi se asigna automaticamente un ID a un nuevo registro
                state.CreatedDate = DateTime.Now;
                state.CountryId = countryId;
                state.country = await _context.Countries.FirstOrDefaultAsync(c => c.Id ==  countryId);
                state.ModifiedDate = null;

                _context.States.Add(state); 
                await _context.SaveChangesAsync(); 
                return state;
            }
            catch (DbUpdateException dbUpdateException)
            {
                //Esta excepción me captura un mensaje cuando el estado ya existe (Duplicados)
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message); //Coallesences Notation --> ??
            }
        }

        public async Task<State> GetStateByIdAsync(Guid id)
        {
            return await _context.States.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<State> EditStateAsync(State state, Guid id)
        {
            try
            {
                state.ModifiedDate = DateTime.Now;

                _context.States.Update(state); // El método Update que es de EF CORE me sirve para actualizar un objeto
                await _context.SaveChangesAsync(); //Aqui ya estoy yendo a la BD para hacer el INSERT en la table Countries

                return state;
            }
            catch (DbUpdateException dbUpdateException)
            {
                //Esta excepción me captura un mensaje cuando el pais ya existe (Duplicados)
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message); //Coallesences Notation --> ??
            }
        }

        public async Task<State> DeleteStateAsync(Guid id)
        {
            try
            {
                var state = await _context.States.FirstOrDefaultAsync(x => x.Id == id);
                if (state == null) return null; //Si el estaedo no existe retorna null

                _context.States.Remove(state);
                await _context.SaveChangesAsync();

                return state;
            }
            catch (DbUpdateException dbUpdateException)
            {
                //Esta excepción me captura un mensaje cuando el pais ya existe (Duplicados)
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message); //Coallesences Notation --> ??
            }
            
        }
    }
}
