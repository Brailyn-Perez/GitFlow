using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ShopApp.Domain.Entity.Configurations
{
    public partial class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> entity)
        {
            entity.Property(e => e.Categoryid).HasColumnName("categoryid");
            entity.Property(e => e.Categoryname)
                .IsRequired()
                .HasMaxLength(15) 
                .HasColumnName("categoryname");
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
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("description");
            entity.Property(e => e.ModifyDate)
                .HasColumnType("datetime")
                .HasColumnName("modify_date");
            entity.Property(e => e.ModifyUser).HasColumnName("modify_user");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Category> entity);
    }
}
