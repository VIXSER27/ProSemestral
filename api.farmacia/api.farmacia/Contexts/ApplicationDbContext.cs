using Microsoft.EntityFrameworkCore;
using ApiLogin.Models;


namespace ApiLogin.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Medicamentos> Medicamentos { get; set; }

    }
}