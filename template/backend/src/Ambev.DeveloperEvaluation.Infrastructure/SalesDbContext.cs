using Microsoft.EntityFrameworkCore;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Infrastructure
{
    public class SalesDbContext : DbContext
    {
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleItem> SaleItems { get; set; }

        public SalesDbContext(DbContextOptions<SalesDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configura Sale
            modelBuilder.Entity<Sale>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.OwnsMany(s => s.Items, i =>
                {
                    i.WithOwner().HasForeignKey("SaleId");
                    i.HasKey(x => x.Id);
                });
            });

            // Configura SaleItem
            modelBuilder.Entity<SaleItem>().Property(x => x.Id).ValueGeneratedNever();
        }
    }
}
