using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace WebApi.Entities
{
    public class DatabaseContext : DbContext
    {

        public DatabaseContext()
        { 
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public virtual DbSet<Comanda> Comanda { get; set; }

        public virtual DbSet<Comanda_itens> Comanda_itens { get; set; }
        public virtual DbSet<Itens> Itens { get; set; }

        

    }
}
