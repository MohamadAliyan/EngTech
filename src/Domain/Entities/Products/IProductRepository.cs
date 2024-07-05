namespace EngTech.Domain.Entities.Products;

public interface IProductRepository : IRepository<Product>
{
    bool ExistTitleProduct(string title);
    bool ExistTitleProduct(string title, int productId);
    bool CheckInvetoryProduct(int productId, int count);
}