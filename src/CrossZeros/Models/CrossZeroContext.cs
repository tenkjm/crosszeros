using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CrossZeros.Models
{
    public class CrossZeroContext:IdentityDbContext<CrossZeroUser>
    {
        public CrossZeroContext()
        {
            Database.EnsureCreated();
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<GameState> GameStates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connString = Startup.Configuration["ConnectionStrings:DefaultConnection"];
            optionsBuilder.UseSqlServer(connString);
            base.OnConfiguring(optionsBuilder);
        }

    }
}
