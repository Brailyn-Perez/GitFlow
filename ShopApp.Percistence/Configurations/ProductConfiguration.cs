using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ShopApp.Domain.Entity.Configurations
{
    public partial class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> entity)
        {
            entity.Property(e => e.Productid).HasColumnName("productid");
            entity.Property(e => e.Categoryid).HasColumnName("categoryid");
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
            entity.Property(e => e.Discontinued).HasColumnName("discontinued");
            entity.Property(e => e.ModifyDate)
                .HasColumnType("datetime")
                .HasColumnName("modify_date");
            entity.Property(e => e.ModifyUser).HasColumnName("modify_user");
            entity.Property(e => e.Productname)
                .IsRequired()
                .HasMaxLength(40)
                .HasColumnName("productname");
            entity.Property(e => e.Supplierid).HasColumnName("supplierid");
            entity.Property(e => e.Unitprice)
                .HasColumnType("money")
                .HasColumnName("unitprice");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Product> entity);
    }
}
