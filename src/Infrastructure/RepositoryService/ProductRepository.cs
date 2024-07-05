using EngTech.Domain.Entities.Products;
using EngTech.Infrastructure.Common;
using EngTech.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EngTech.Infrastructure.RepositoryService;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    private readonly DbSet<Product> _entity;

    public ProductRepository(ApplicationDbContext context) : base(context)
    {
        _entity = context.Set<Product>();
    }

    public bool ExistTitleProduct(string title)
    {
        return _entity.Any(p => p.Title == title);
    }

    public bool ExistTitleProduct(string title, int productId)
    {
        return _entity.Any(p => p.Title == title && p.Id != productId);
    }

    public bool CheckInvetoryProduct(int productId, int count)
    {
        int inventoryCount = _entity.Single(p => p.Id == productId).InventoryCount;
        return inventoryCount >= count;
    }
}