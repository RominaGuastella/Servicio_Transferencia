using Microsoft.EntityFrameworkCore;
using Transferencias.Persistence.Entities;

namespace Transferencias.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { 

        }

        public DbSet<Transferencia> Transferencias { get; set; }
    }
}