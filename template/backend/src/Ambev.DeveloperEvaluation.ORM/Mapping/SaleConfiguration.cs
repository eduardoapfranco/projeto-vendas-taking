using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("Sales");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id)
                   .HasColumnType("uuid")
                   .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(s => s.SaleNumber)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(s => s.SaleDate)
                   .IsRequired();

            builder.Property(s => s.BranchId)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(s => s.BranchName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(s => s.CustomerId)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(s => s.CustomerName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(s => s.IsCancelled)
                   .IsRequired();

            builder.OwnsMany(s => s.Items, a =>
            {
                a.ToTable("SaleItems");
                a.WithOwner().HasForeignKey("SaleId");
                a.HasKey(si => si.Id);
                a.Property(si => si.Id)
                 .HasColumnType("uuid")
                 .HasDefaultValueSql("gen_random_uuid()");

                a.Property(si => si.ProductId)
                 .IsRequired()
                 .HasMaxLength(50);

                a.Property(si => si.ProductName)
                 .IsRequired()
                 .HasMaxLength(100);

                a.Property(si => si.UnitPrice)
                 .IsRequired();

                a.Property(si => si.Quantity)
                 .IsRequired();

                a.Property(si => si.DiscountPercentage)
                 .IsRequired();

                a.Property(si => si.Total)
                 .IsRequired();

                a.Property(si => si.IsCancelled)
                 .IsRequired();
            });
        }
    }
}
