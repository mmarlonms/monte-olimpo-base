using Microsoft.EntityFrameworkCore;
using MonteOlimpo.Domain.Models;

namespace MonteOlimpo.Sample.Data
{
    public class SampleContext : DbContext
    {
        public DbSet<Deus> Deus { get; set; }

        public SampleContext(DbContextOptions<SampleContext> options) :
           base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Deus>().HasKey(c => c.Id);
        }
    }
}
