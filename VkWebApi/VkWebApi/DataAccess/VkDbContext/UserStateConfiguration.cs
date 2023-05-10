using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VkWebApi.DataAccess.Models;

namespace VkWebApi.DataAccess.VkDbContext;

public class UserStateConfiguration : IEntityTypeConfiguration<UserState>
{
    public void Configure(EntityTypeBuilder<UserState> builder)
    {
        builder.Property(u => u.State)
            .HasConversion(s => s.ToString(), s => Enum.Parse<Status>(s));
        
        
        builder.Property(u => u.Id).HasColumnName("id");
        builder.Property(u => u.State).HasColumnName("code");
        builder.Property(u => u.Description).HasColumnName("description");

        builder.HasData(new UserState
            {
                State = Status.Active, Description = "Active user", Id = 1
            }, 
            new UserState
            {
                State = Status.Blocked, Description = "Deleted user", Id = 2
            });
    }
}