using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ShopApp.Domain.Entity.Configurations
{
    public partial class ShipperConfiguration : IEntityTypeConfiguration<Shipper>
    {
        public void Configure(EntityTypeBuilder<Shipper> entity)
        {
            entity.Property(e => e.Shipperid).HasColumnName("shipperid");
            entity.Property(e => e.Address)
                .HasMaxLength(60)
                .HasColumnName("address");
            entity.Property(e => e.City)
                .HasMaxLength(60)
                .HasColumnName("city");
            entity.Property(e => e.Country).HasColumnName("country");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creation_date");
            entity.Property(e => e.CreationUser)
                .HasDefaultValue(1)
                .HasColumnName("creation_user");
            entity.Property(e => e.DeleteDate).HasColumnName("delete_date"); 
            entity.Property(e => e.DeleteUser).HasColumnName("delete_user");
            entity.Property(e => e.Deleted).HasColumnName("deleted");
            entity.Property(e => e.ModifyDate)
                .HasColumnType("datetime")
                .HasColumnName("modify_date");
            entity.Property(e => e.ModifyUser).HasColumnName("modify_user");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(40)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .IsRequired()
                .HasMaxLength(24)
                .HasColumnName("phone");
            entity.Property(e => e.Postalcode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("postalcode");
            entity.Property(e => e.Region)
                .HasMaxLength(15)
                .HasColumnName("region");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Shipper> entity);
    }
}
