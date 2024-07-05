namespace EngTech.Application.Contract.Queries.Products;

public class GetProductByIdDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int InventoryCount { get; set; }
    public long FinalPrice { get; set; }
    public long Price { get; set; }
    public int Discount { get; set; }
}