using Microsoft.EntityFrameworkCore;
using ShoppingAPI_Jueves_2023II.DAL__DataAccessLayer_.Entities;

namespace ShoppingAPI_Jueves_2023II.DAL__DataAccessLayer_
{
    public class DataBaseContext : DbContext
    {
        //Asi me conecto a la BD por medio de este constructor
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
            
        }

        //Este método que es propio de EF CORE me sirve para configurar unos indices de cada campo de una tabla en BD
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasIndex(c => c.Name).IsUnique(); //Aqui creo un índice del campo Name para la tabla Countries...
                                                                             //El indice es para que no exitan valores duplicados en en campo name
        }

        #region DbSets

        public DbSet<Country> Countries { get; set; }
                                  
        #endregion
    }
}
