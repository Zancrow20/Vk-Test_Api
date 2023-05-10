using DtoLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VkWebApi.DataAccess.Models;

namespace VkWebApi.DataAccess.VkDbContext;

public class UserGroupConfiguration : IEntityTypeConfiguration<UserGroup>
{
    public void Configure(EntityTypeBuilder<UserGroup> builder)
    {
        builder.Property(u => u.Role)
            .HasConversion(r => r.ToString(), r => Enum.Parse<Role>(r));
        
        builder.Property(u => u.Id).HasColumnName("id");
        builder.Property(u => u.Role).HasColumnName("code");
        builder.Property(u => u.Description).HasColumnName("description");
        
        
        builder.HasData(new UserGroup
            {
                Role = Role.Admin, Description = "Admin", Id = 1
            }, 
            new UserGroup
            {
                Role = Role.User, Description = "Simple user", Id = 2
            });
    }
}