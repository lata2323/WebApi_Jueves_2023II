using ShoppingAPI_Jueves_2023II.DAL__DataAccessLayer_.Entities;

namespace ShoppingAPI_Jueves_2023II.Domain.Interfaces
{
    public interface ICountryService
    {
        //Ilist
        //Icollection
        //IEnumerable es el más adecuado pero las anteriores también listan de diferentes maneras. Lista de forma estática
        Task<IEnumerable<Country>> GetCountriesAsync(); //Una firma de método
        Task<Country> CreateCountryAsync(Country country); //Para crear un pais y se necesita el objeto completo de Country
        Task<Country> GetCountryByIdAsync(Guid id);
        Task<Country> GetCountryByNameAsync(string name);
        Task<Country> EditCountryAsync(Country country);
        Task<Country> DeleteCountryAsync(Guid id);
    }
}
