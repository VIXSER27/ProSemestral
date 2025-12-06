using ApiLogin.Contexts;
using ApiLogin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiLogin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicamentosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MedicamentosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/medicamentos
        [HttpGet]
        public async Task<ActionResult<List<Medicamentos>>> GetMedicamentos()
        {
            return await _context.Medicamentos.ToListAsync();
        }

        // GET: api/medicamentos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Medicamentos>> GetMedicamento(int id)
        {
            var medicamento = await _context.Medicamentos.FindAsync(id);
            if (medicamento == null)
            {
                return NotFound();
            }
            return Ok(medicamento);
        }

        // POST: api/medicamentos
        [HttpPost]
        public async Task<ActionResult> CrearMedicamento(Medicamentos medicamento)
        {
            if (medicamento == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Medicamentos.Add(medicamento);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMedicamento), new { id = medicamento.id }, medicamento);
        }

        // PUT: api/medicamentos/5
        [HttpPut("{id}")]
        public async Task<ActionResult> ActualizarMedicamento(int id, Medicamentos medicamentoActualizado)
        {
            if (id != medicamentoActualizado.id || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existente = await _context.Medicamentos.FindAsync(id);
            if (existente == null)
            {
                return NotFound();
            }

            existente.nombre = medicamentoActualizado.nombre;
            existente.imagen = medicamentoActualizado.imagen;
            existente.cantidad_disponible = medicamentoActualizado.cantidad_disponible;
            existente.precio_unitario = medicamentoActualizado.precio_unitario;

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "Error al actualizar el medicamento");
            }
        }

        // DELETE: api/medicamentos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarMedicamento(int id)
        {
            var medicamento = await _context.Medicamentos.FindAsync(id);
            if (medicamento == null)
            {
                return NotFound();
            }

            try
            {
                _context.Medicamentos.Remove(medicamento);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "Error al eliminar el medicamento");
            }
        }

        // PATCH: api/medicamentos/5/reabastecer
        [HttpPatch("{id}/reabastecer")]
        public async Task<ActionResult> Reabastecer(int id, [FromBody] dynamic body)
        {
            int extra = (int)body.extra;
            var medicamento = await _context.Medicamentos.FindAsync(id);
            if (medicamento == null)
            {
                return NotFound();
            }

            medicamento.cantidad_disponible += extra;
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Reabastecido" });
        }
    }
}
