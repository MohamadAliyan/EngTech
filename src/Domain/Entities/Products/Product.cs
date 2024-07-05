namespace EngTech.Domain.Entities.Products;

public class Product : BaseAuditableEntity
{
    public string Title { get; set; }
    public int InventoryCount { get; set; }
    public long Price { get; set; }
    public int Discount { get; set; }
    public long FinalPrice { get; set; }
}
