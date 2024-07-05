using EngTech.Application.Contract.Queries.Products;
using EngTech.Domain.Entities.Products;
using EngTech.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace EngTech.Application.Handlers.Queries.Products;

public class
    GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, GetProductByIdDto>
{
    private readonly IMemoryCache _memoryCache;
    private readonly IProductRepository _productRepository;

    public GetProductByIdQueryHandler(IProductRepository productRepository,
        IMemoryCache memoryCache)
    {
        _productRepository = productRepository;
        _memoryCache = memoryCache;
    }

    public async Task<GetProductByIdDto> Handle(GetProductByIdQuery request,
        CancellationToken cancellationToken)
    {
        string cacheKey = Constant.GetProductId + request.Id;
        GetProductByIdDto productCache;
        if (_memoryCache.TryGetValue(cacheKey, out productCache))
        {
            return productCache;
        }

        try
        {
            Product foundProduct = _productRepository.GetBy(p => p.Id == request.Id)
                .AsNoTracking().Single();

            GetProductByIdDto product = new()
            {
                Id = foundProduct.Id,
                InventoryCount = foundProduct.InventoryCount,
                Title = foundProduct.Title,
                FinalPrice = foundProduct.FinalPrice,
                Discount = foundProduct.Discount,
                Price = foundProduct.Price
            };
            MemoryCacheEntryOptions cacheEntryOptions =
                new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(60));

            _memoryCache.Set(cacheKey, product, cacheEntryOptions);
            return product;
        }
        catch (Exception e)
        {
            throw new Exception("Product Not Found");
        }
    }
}