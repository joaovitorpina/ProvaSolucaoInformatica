using Microsoft.EntityFrameworkCore;
using ProvaSolucaoInformatica.Databases.Default.Entities;

namespace ProvaSolucaoInformatica.Databases.Default
{
    public class DefaultDbContext : DbContext
    {
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Usuario> Usuarios { get; set; }

        public virtual DbSet<Cliente> Clientes { get; set; }
    }
}