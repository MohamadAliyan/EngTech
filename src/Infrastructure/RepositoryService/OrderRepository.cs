using EngTech.Domain.Entities.Orders;
using EngTech.Infrastructure.Common;
using EngTech.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EngTech.Infrastructure.RepositoryService;

public class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    private readonly DbSet<Order> _entity;

    public OrderRepository(ApplicationDbContext context) : base(context)
    {
        _entity = context.Set<Order>();
    }
}