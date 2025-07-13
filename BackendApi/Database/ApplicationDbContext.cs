using Microsoft.EntityFrameworkCore;

namespace BackendApi.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Produto> produtos { get; set; }
    }
}
