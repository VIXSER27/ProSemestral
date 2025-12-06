using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("medicamentos")]
public class Medicamentos
{
    [Key]
    public int id { get; set; }

    [Required]
    [StringLength(100)]
    public string nombre { get; set; }

    // URL o ruta de la imagen del medicamento
    [StringLength(200)]
    public string imagen { get; set; }

    [Required]
    public int cantidad_disponible { get; set; }

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal precio_unitario { get; set; }
}