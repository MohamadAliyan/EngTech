using EngTech.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EngTech.Infrastructure.Persistence.Configurations;

public class ProductConfiguration : BaseConfiguration<Product>
{
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        base.Configure(builder);
    }
}