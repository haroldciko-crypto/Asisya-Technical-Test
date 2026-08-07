using Asisya.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Asisya.Infrastructure.Persistence.Configurations;

public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
{
    public void Configure(EntityTypeBuilder<OrderDetail> builder)
    {
        builder.ToTable("OrderDetails");

        // Clave primaria compuesta
        builder.HasKey(od => new { od.OrderID, od.ProductID });

        builder.Property(od => od.UnitPrice)
            .HasColumnType("decimal(18,2)");

        builder.Property(od => od.Quantity)
            .IsRequired();

        builder.Property(od => od.Discount)
            .HasDefaultValue(0);

        builder.HasOne(od => od.Order)
            .WithMany(o => o.OrderDetails)
            .HasForeignKey(od => od.OrderID)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(od => od.Product)
            .WithMany(p => p.OrderDetails)
            .HasForeignKey(od => od.ProductID)
            .OnDelete(DeleteBehavior.Restrict);
    }
}