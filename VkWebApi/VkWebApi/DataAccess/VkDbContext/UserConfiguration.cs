using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VkWebApi.DataAccess.Models;

namespace VkWebApi.DataAccess.VkDbContext;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasOne(e => e.UserGroup)
            .WithMany()
            .HasForeignKey(e => e.UserGroupId)
            .IsRequired();
        
        builder
            .HasOne(e => e.UserState)
            .WithMany()
            .HasForeignKey(e => e.UserStateId)
            .IsRequired();
        
        builder.Property(u => u.Id).HasColumnName("id");
        builder.Property(u => u.Login).HasColumnName("login");
        builder.Property(u => u.Password).HasColumnName("password");
        builder.Property(u => u.Created_Date).HasColumnName("created_date");
        builder.Property(u => u.UserGroupId).HasColumnName("user_group_id");
        builder.Property(u => u.UserStateId).HasColumnName("user_state_id");
    }
}