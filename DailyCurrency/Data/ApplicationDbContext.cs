using DailyCurrency.Models;
using Microsoft.EntityFrameworkCore;

namespace DailyCurrency.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
        }

        public DbSet<Currency> Currencies { get; set; }
    }
}
