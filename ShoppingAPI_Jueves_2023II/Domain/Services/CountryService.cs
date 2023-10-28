using Microsoft.EntityFrameworkCore;
using ShoppingAPI_Jueves_2023II.DAL__DataAccessLayer_;
using ShoppingAPI_Jueves_2023II.DAL__DataAccessLayer_.Entities;
using ShoppingAPI_Jueves_2023II.Domain.Interfaces;

namespace ShoppingAPI_Jueves_2023II.Domain.Services
{
    public class CountryService : ICountryService
    {
        public readonly DataBaseContext _context;

        public CountryService(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Country>> GetCountriesAsync() //ICollection sirve para extraer los datos de forma más dinámica
        {
            return await _context.Countries.ToListAsync(); //Aqui lo que hago es traerme todos los datos que tengo en mi tabla Countries
        }

        public async Task<Country> CreateCountryAsync(Country country)
        {
            try
            {
                country.Id = Guid.NewGuid(); //Asi se asigna automaticamente un ID a un nuevo registro
                country.CreatedDate = DateTime.Now;

                _context.Countries.Add(country); // Aqui se esta creando el objeto country en el contexto de mi BD
                await _context.SaveChangesAsync(); //Aqui ya estoy yendo a la BD para hacer el INSERT en la table Countries
                return country;
            }
            catch (DbUpdateException dbUpdateException)
            {
                //Esta excepción me captura un mensaje cuando el pais ya existe (Duplicados)
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message); //Coallesences Notation --> ??
            }
        }


    }
}
