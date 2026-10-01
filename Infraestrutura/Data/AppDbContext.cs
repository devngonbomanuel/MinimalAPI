using Minimal.Dominio.Entities;
using Microsoft.EntityFrameworkCore;

namespace Minimal.Infraestrutura.Data;

public class AppDbContext : DbContext
{        
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Admin> Administradores { get; set; } = default!;
        
        public DbSet<Calcado> Calcados { get; set; } = default!;
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Admin>().HasData(
                new Admin {
                    Id = 1,
                    Email = "admin@teste.com",
                    Senha = "123abc",
                    Perfil = "Admin"
                });
        }
}