using Microsoft.EntityFrameworkCore;

namespace DB
{
    public class TelnetDbContext :DbContext
    {
        public TelnetDbContext(DbContextOptions<TelnetDbContext> options): base(options)
        {

        }

        public DbSet<Tienda> Tienda { get; set; }
        public DbSet<Articulo> Articulo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tienda>().ToTable("tienda");
            modelBuilder.Entity<Articulo>().ToTable("articulo");
        }



    }
}
