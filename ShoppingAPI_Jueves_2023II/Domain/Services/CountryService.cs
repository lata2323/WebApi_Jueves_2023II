using Microsoft.EntityFrameworkCore;
using ShoppingAPI_Jueves_2023II.DAL__DataAccessLayer_;
using ShoppingAPI_Jueves_2023II.DAL__DataAccessLayer_.Entities;
using ShoppingAPI_Jueves_2023II.Domain.Interfaces;
using System.Diagnostics.Metrics;
using System.Xml.Linq;

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

        public async Task<Country> GetCountryByIdAsync(Guid id)
        {
            //return await _context.Countries.FindAsync(id); //FindAsync es un metodo propio del DbContext (DbSet)
            //return await _context.Countries.FirstAsync(x => x.Id == id); //FirstAsync es un método de EF CORE
            return await _context.Countries.FirstOrDefaultAsync(x => x.Id == id); //FirstOrDefaultAsync es un método de EF CORE, es mejor utilizar
                                                                                  //este porque si hay un objeto vacío devuelve el objeto vacio, no un
                                                                                  //error
        }

        public async Task<Country> GetCountryByNameAsync(string name)
        {
            return await _context.Countries.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<Country> EditCountryAsync(Country country)
        {
            try
            {
                country.ModifiedDate = DateTime.Now;

                _context.Countries.Update(country); // El método Update que es de EF CORE me sirve para actualizar un objeto
                await _context.SaveChangesAsync(); //Aqui ya estoy yendo a la BD para hacer el INSERT en la table Countries

                return country;
            }
            catch (DbUpdateException dbUpdateException)
            {
                //Esta excepción me captura un mensaje cuando el pais ya existe (Duplicados)
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message); //Coallesences Notation --> ??
            }
        }

        public async Task<Country> DeleteCountryAsync(Guid id)
        {
            try
            {
                var country = await _context.Countries.FirstOrDefaultAsync(x => x.Id == id);
                if (country == null) return null; //Si el pais no existe retorna null

                _context.Countries.Remove(country);
                await _context.SaveChangesAsync();

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
