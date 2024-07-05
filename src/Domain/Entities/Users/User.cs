using EngTech.Domain.Entities.Orders;

namespace EngTech.Domain.Entities.Users;

public class User : BaseAuditableEntity
{
    public string Name { get; set; }
    public List<Order> Orders { get; set; }
}