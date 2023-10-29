using ShoppingAPI_Jueves_2023II.DAL__DataAccessLayer_.Entities;

namespace ShoppingAPI_Jueves_2023II.Domain.Interfaces
{
    public interface IStateService
    {
        //Ilist
        //Icollection
        //IEnumerable es el más adecuado pero las anteriores también listan de diferentes maneras. Lista de forma estática
        Task<IEnumerable<State>> GetStatesByCountryIdAsync(Guid countryId); //Una firma de método
        Task<State> CreateStateAsync(State state, Guid countryId); //Para crear un pais y se necesita el objeto completo de Country
        Task<State> GetStateByIdAsync(Guid id);
        Task<State> EditStateAsync(State state, Guid id);
        Task<State> DeleteStateAsync(Guid id);
    }
}
