using System.ComponentModel.DataAnnotations;

namespace ShoppingAPI_Jueves_2023II.DAL__DataAccessLayer_.Entities
{
    public class State : AuditBase
    {
        [Display(Name = "Estado/Departamento")] //Para identificar el nombre más fácil
        [MaxLength(50, ErrorMessage = "El campo {0} Debe terminar máximo {1} caracteres")] //Longitud Máxima
        [Required(ErrorMessage = "El campo {0} es obligatorio")] //Campo obligatorio
        public string Name { get; set; } //Varchar 50

        [Display(Name = "País")]
        public Country? country { get; set; } //Este representa un objeto de Country, relación con country en State. El ? dice que no es nuleable

        [Display(Name = "Id País")]
        public Guid CountryId { get; set; } //Fk
    }
}
