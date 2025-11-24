using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ShopApp.Domain.Entity.Configurations
{
    public partial class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> entity)
        {
            entity.Property(e => e.Orderid).HasColumnName("orderid");
            entity.Property(e => e.Custid).HasColumnName("custid");
            entity.Property(e => e.Empid).HasColumnName("empid");
            entity.Property(e => e.Freight)
                .HasColumnType("money")
                .HasColumnName("freight");
            entity.Property(e => e.Orderdate)
                .HasColumnType("datetime")
                .HasColumnName("orderdate");
            entity.Property(e => e.Requireddate)
                .HasColumnType("datetime")
                .HasColumnName("requireddate");
            entity.Property(e => e.Shipaddress)
                .IsRequired() 
                .HasMaxLength(60)
                .HasColumnName("shipaddress");
            entity.Property(e => e.Shipcity)
                .IsRequired()
                .HasMaxLength(15)
                .HasColumnName("shipcity");
            entity.Property(e => e.Shipcountry)
                .IsRequired()
                .HasMaxLength(15)
                .HasColumnName("shipcountry");
            entity.Property(e => e.Shipname)
                .IsRequired()
                .HasMaxLength(40)
                .HasColumnName("shipname");
            entity.Property(e => e.Shippeddate)
                .HasColumnType("datetime")
                .HasColumnName("shippeddate");
            entity.Property(e => e.Shipperid).HasColumnName("shipperid");
            entity.Property(e => e.Shippostalcode)
                .HasMaxLength(10)
                .HasColumnName("shippostalcode");
            entity.Property(e => e.Shipregion)
                .HasMaxLength(15)
                .HasColumnName("shipregion");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Order> entity);
    }
}
