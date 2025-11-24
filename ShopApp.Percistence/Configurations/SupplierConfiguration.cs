using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ShopApp.Domain.Entity.Configurations
{
    public partial class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> entity)
        {
            entity.Property(e => e.Supplierid).HasColumnName("supplierid");
            entity.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(60)
                .HasColumnName("address");
            entity.Property(e => e.City)
                .IsRequired()
                .HasMaxLength(15)
                .HasColumnName("city");
            entity.Property(e => e.Companyname)
                .IsRequired()
                .HasMaxLength(40)
                .HasColumnName("companyname");
            entity.Property(e => e.Contactname)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("contactname"); 
            entity.Property(e => e.Contacttitle)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("contacttitle");
            entity.Property(e => e.Country)
                .IsRequired()
                .HasMaxLength(15)
                .HasColumnName("country");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creation_date");
            entity.Property(e => e.CreationUser)
                .HasDefaultValue(1)
                .HasColumnName("creation_user");
            entity.Property(e => e.DeleteDate)
                .HasColumnType("datetime")
                .HasColumnName("delete_date");
            entity.Property(e => e.DeleteUser).HasColumnName("delete_user");
            entity.Property(e => e.Deleted).HasColumnName("deleted");
            entity.Property(e => e.Fax)
                .HasMaxLength(24)
                .HasColumnName("fax");
            entity.Property(e => e.ModifyDate)
                .HasColumnType("datetime")
                .HasColumnName("modify_date");
            entity.Property(e => e.ModifyUser).HasColumnName("modify_user");
            entity.Property(e => e.Phone)
                .IsRequired()
                .HasMaxLength(24)
                .HasColumnName("phone");
            entity.Property(e => e.Postalcode)
                .HasMaxLength(10)
                .HasColumnName("postalcode");
            entity.Property(e => e.Region)
                .HasMaxLength(15)
                .HasColumnName("region");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Supplier> entity);
    }
}
