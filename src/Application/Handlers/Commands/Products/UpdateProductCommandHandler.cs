using EngTech.Application.Contract.Commands.Products;
using EngTech.Application.Contract.Common.Models;
using EngTech.Application.Contract.Queries.Products;
using EngTech.Domain.Entities.Products;
using EngTech.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace EngTech.Application.Handlers.Commands.Products;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, int>
{
    private readonly IOptions<AppSettings> _configuration;
    private readonly IMemoryCache _memoryCache;
    private readonly IProductRepository _productRepository;

    public UpdateProductCommandHandler(IProductRepository productRepository,
        IOptions<AppSettings> configuration, IMemoryCache memoryCache)
    {
        _productRepository = productRepository;
        _configuration = configuration;
        _memoryCache = memoryCache;
    }

    public async Task<int> Handle(UpdateProductCommand request,
        CancellationToken cancellationToken)
    {
        //get product
        Product? found = _productRepository.GetBy(p => p.Id == request.Id).Single();

        if (found != null)
        {
            found.Price = request.Price;
            found.Discount = request.Discount;
            found.Title = request.Title;
            found.InventoryCount = request.InventoryCount;
            found.FinalPrice = CalculatePrice(request.Price, request.Discount);

            _productRepository.Update(found);
            int res = await _productRepository.SaveChanges();

            //romove from cache
            string cacheKey = Constant.GetProductId + request.Id;
            _memoryCache.Remove(cacheKey);

            //set cache
            MemoryCacheEntryOptions cacheEntryOptions =
                new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(60));
            GetProductByIdDto product = new()
            {
                Id = found.Id,
                Price = found.Price,
                InventoryCount = found.InventoryCount,
                Title = found.Title
            };
            _memoryCache.Set(cacheKey, product, cacheEntryOptions);

            return res;
        }

        throw new Exception("Product Not Found");
    }


    private long CalculatePrice(long price, int discount)
    {
        long discount_amount = price * discount / 100;
        long price_after_discount = price - discount_amount;
        return price_after_discount;
    }
}