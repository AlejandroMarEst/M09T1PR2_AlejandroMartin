using M09T1PR2API_AlejandroMartin.Model;
using Microsoft.EntityFrameworkCore;

namespace M09T1PR2API_AlejandroMartin.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
    }
}
