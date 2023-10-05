using System.ComponentModel.DataAnnotations;

namespace ShoppingAPI_Jueves_2023II.DAL__DataAccessLayer_.Entities
{
    public class Country : AuditBase
    {
        [Display(Name = "País")] //Para identificar el nombre más fácil
        [MaxLength(50, ErrorMessage = "El campo {0} Debe terminar máximo {1} caracteres")] //Longitud Máxima
        [Required(ErrorMessage = "El campo {0} es obligatorio")] //Campo obligatorio
        public string Name { get; set; }
    }
}
