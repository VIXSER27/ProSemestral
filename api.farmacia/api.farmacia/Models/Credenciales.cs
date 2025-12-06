using System.ComponentModel.DataAnnotations;

namespace ApiLogin.Models
{
    public class Credenciales
    {
        [Required]
        [EmailAddress]
        public string? correo { get; set; }
        [Required]
        public string? contrasena { get; set; }
    }
}