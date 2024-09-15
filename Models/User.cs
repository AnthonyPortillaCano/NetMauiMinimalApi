using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo usuario es obligatorio")]
        public string? Nombre { get; set; }
        [Required(ErrorMessage = "El campo clave es obligatorios")]
        public string? Clave { get; set; }

        public string? Token { get; set; }
    }
}
