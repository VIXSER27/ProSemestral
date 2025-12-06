public class Medicamentos
{
    public int id { get; set; }
    public string nombre { get; set; }

    // URL o ruta de la imagen del medicamento
    public string imagen { get; set; }

    public int cantidad_disponible { get; set; }
    public decimal precio_unitario { get; set; }
}