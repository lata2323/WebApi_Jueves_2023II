using ShoppingAPI_Jueves_2023II.DAL__DataAccessLayer_.Entities;

namespace ShoppingAPI_Jueves_2023II.DAL__DataAccessLayer_
{
    public class SeederDb
    {
        private readonly DataBaseContext _context;

        public SeederDb(DataBaseContext context)
        {
            _context = context;
        }

        //Crearemos un metodo llamado seeder async, es una especie de main()
        //Este método tendrá la responsaabilidad de prepoblar las diferentes tablas de la BD

        public async Task SeederAsync()
        {
            //se agrega un metodo propio de EF que hace las veces del comando 'update-database'
            //Un método que crea la BD inmediatamente se ejecuta la Api
            await _context.Database.EnsureCreatedAsync();

            //A partir de aqui se crearan métodos que sirvan para prepoblar la BD
            await PopulateCountriesAsync();

            await _context.SaveChangesAsync(); //Esta linea guarda los datos en la BD
        }

        #region private methods
        private async Task PopulateCountriesAsync()
        {
            if(!_context.Countries.Any()) //El método Any negado indica si la tabla Countries no tiene registros
            {
                //Asi se crea un objeto país con sus respectivos estados
                _context.Countries.Add(new Country
                {
                    CreatedDate = DateTime.Now,
                    Name = "Colombia",
                    States = new List<State>()
                    {
                        new State
                        {
                            CreatedDate = DateTime.Now,
                            Name = "Antioquia"
                        },

                        new State
                        {
                            CreatedDate = DateTime.Now,
                            Name = "Cundinamarca"
                        }
                    }
                });

                //Aqui se crea otro nuevo país
                _context.Countries.Add(new Country
                {
                    CreatedDate = DateTime.Now,
                    Name = "Argentina",
                    States = new List<State>()
                    {
                        new State
                        {
                            CreatedDate = DateTime.Now,
                            Name = "Buenos Aires"
                        }
                    }
                });
            }
        }


        #endregion
    }
}
