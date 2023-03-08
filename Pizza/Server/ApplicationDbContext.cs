using Microsoft.EntityFrameworkCore;
using Pizza.Shared.Models;

namespace Pizza.Server
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {    
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Tamano> Tamanos { get; set; }
        public DbSet<Ingrediente> Ingredientes { get; set; }
        public DbSet<PizzaModel> Pizzas { get; set; }
    }
}
