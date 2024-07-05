using EngTech.Domain.Entities.Products;
using EngTech.Domain.Entities.Users;

namespace EngTech.Domain.Entities.Orders;

public class Order : BaseAuditableEntity
{
    public int ProductId { get; set; }
    public virtual Product Product { get; set; }

    public int UserId { get; set; }
    public virtual User User { get; set; }

    public DateTime CreationDate { get; set; }
    public int Count { get; set; }
    public long Amount { get; set; }
}