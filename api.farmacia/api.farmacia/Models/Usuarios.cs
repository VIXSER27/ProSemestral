using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("usuarios")]
public class Usuarios
{
    [Key]
    public int id { get; set; }

    [Required]
    [StringLength(50)]
    public string nombre { get; set; }

    [Required]
    [StringLength(50)]
    public string apellido { get; set; }

    [Required]
    [StringLength(100)]
    public string correo { get; set; }

    [Required]
    [StringLength(100)]
    public string contrasena { get; set; }

    // Campo para el rol
    [Required]
    [StringLength(20)]
    public string rol { get; set; }   // admin / user
}
