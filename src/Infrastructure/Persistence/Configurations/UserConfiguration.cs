using EngTech.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EngTech.Infrastructure.Persistence.Configurations;

public class UserConfiguration : BaseConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);
    }
}