using EngTech.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EngTech.Infrastructure.Persistence.Configurations;

public class OrderConfiguration : BaseConfiguration<Order>
{
    public override void Configure(EntityTypeBuilder<Order> builder)
    {
        base.Configure(builder);
    }
}