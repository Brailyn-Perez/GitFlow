using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ShopApp.Domain.Entity.Configurations
{
    public partial class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> entity)
        {
            entity.HasKey(e => new { e.Orderid, e.Productid });

            entity.Property(e => e.Orderid).HasColumnName("orderid");
            entity.Property(e => e.Productid).HasColumnName("productid");
            entity.Property(e => e.Discount)
                .HasColumnType("numeric(4, 3)")
                .HasColumnName("discount");
            entity.Property(e => e.Qty)
                .HasDefaultValue((short)1)
                .HasColumnName("qty");
            entity.Property(e => e.Unitprice)
                .HasColumnType("money")
                .HasColumnName("unitprice");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<OrderDetail> entity);
    }
}
