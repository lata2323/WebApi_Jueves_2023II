using System.ComponentModel.DataAnnotations;

namespace ShoppingAPI_Jueves_2023II.DAL__DataAccessLayer_.Entities
{
    public class AuditBase
    {
        //Estos DataAnotations solo alteran la propiedad ID porque está inmediatamente anterior a esta
        [Key] //PK
        [Required] //Significa que este campo es obligatorio
        public virtual Guid Id { get; set; } //Pk de todas las tablas...Guid es un código para que sea altamente
                                             //seguro y se genera de forma automática
        public virtual DateTime? CreatedDate { get; set; } //Para guardar todo registro nuevo con su fecha
        public virtual DateTime? ModifiedDate { get; set; } //Para guardar todo registro que se modificó con
                                                           //su fecha... el ? quiere decir que no es
                                                           //obligatorio el campo
    }
}
