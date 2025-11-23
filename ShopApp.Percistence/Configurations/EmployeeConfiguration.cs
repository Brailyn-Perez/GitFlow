using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ShopApp.Domain.Entity.Configurations
{
    public partial class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> entity)
        {
            entity.HasKey(e => e.Empid);

            entity.Property(e => e.Empid).HasColumnName("empid");
            entity.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(60)
                .HasColumnName("address");
            entity.Property(e => e.Birthdate)
                .HasColumnType("datetime")
                .HasColumnName("birthdate");
            entity.Property(e => e.City)
                .IsRequired()
                .HasMaxLength(15)
                .HasColumnName("city");
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
            entity.Property(e => e.Deleted)
                .HasDefaultValue(true)
                .HasColumnName("deleted");
            entity.Property(e => e.Firstname)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnName("firstname");
            entity.Property(e => e.Hiredate)
                .HasColumnType("datetime")
                .HasColumnName("hiredate");
            entity.Property(e => e.Lastname)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("lastname");
            entity.Property(e => e.Mgrid).HasColumnName("mgrid");
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
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("title");
            entity.Property(e => e.Titleofcourtesy)
                .IsRequired()
                .HasMaxLength(25)
                .HasColumnName("titleofcourtesy");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Employee> entity);
    }
}
