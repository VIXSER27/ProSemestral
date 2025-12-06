using ApiLogin.Contexts;
using ApiLogin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiLogin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly ApplicationDbContext applicationDbContext;

        public UsuariosController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Usuarios>>> GetUsuarios()
        {
            return await applicationDbContext.Usuarios.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuarios>> GetUsuario(int id)
        {
            var usuario = await applicationDbContext.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<ActionResult> CrearUsuario(Usuarios usuario)
        {
            if (usuario == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Asignar rol por defecto
                usuario.rol = "user";

                applicationDbContext.Usuarios.Add(usuario);
                await applicationDbContext.SaveChangesAsync();

                return CreatedAtAction(nameof(GetUsuario), new { id = usuario.id }, usuario);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Error al crear el usuario: {ex.InnerException?.Message}");
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<Usuarios>> Login(Credenciales credencial)
        {
            if (credencial == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var usuario = await applicationDbContext.Usuarios
                .FirstOrDefaultAsync(u => u.correo == credencial.correo);

            if (usuario == null)
            {
                return Unauthorized();
            }

            if (usuario.contrasena != credencial.contrasena)
            {
                return Unauthorized();
            }

            // No enviar la contraseña
            usuario.contrasena = null;

            // === AQUI VA EL RETURN IMPORTANTE ===
            return Ok(new
            {
                id = usuario.id,
                nombre = usuario.nombre,
                apellido = usuario.apellido,
                correo = usuario.correo,
                rol = usuario.rol   // <-- NECESARIO para redirigir
            });
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> ActualizarUsuario(int id, Usuarios usuarioActualizado)
        {
            if (id != usuarioActualizado.id || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var usuarioExistente = await applicationDbContext.Usuarios.FindAsync(id);
            if (usuarioExistente == null)
            {
                return NotFound();
            }

            usuarioExistente.nombre = usuarioActualizado.nombre;
            usuarioExistente.apellido = usuarioActualizado.apellido;
            usuarioExistente.correo = usuarioActualizado.correo;
            usuarioExistente.contrasena = usuarioActualizado.contrasena;

            try
            {
                await applicationDbContext.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "Error al actualizar el usuario");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarUsuario(int id)
        {
            var usuario = await applicationDbContext.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            try
            {
                applicationDbContext.Usuarios.Remove(usuario);
                await applicationDbContext.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "Error al eliminar el usuario");
            }
        }
    }
}