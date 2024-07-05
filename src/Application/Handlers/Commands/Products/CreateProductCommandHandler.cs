using EngTech.Application.Contract.Commands.Products;
using EngTech.Application.Contract.Common.Models;
using EngTech.Domain.Entities.Products;
using MediatR;
using Microsoft.Extensions.Options;

namespace EngTech.Application.Handlers.Commands.Products;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
{
    private readonly IOptions<AppSettings> _configuration;
    private readonly IProductRepository _productRepository;

    public CreateProductCommandHandler(IProductRepository productRepository,
        IOptions<AppSettings> configuration)
    {
        _productRepository = productRepository;
        _configuration = configuration;
    }

    public async Task<int> Handle(CreateProductCommand request,
        CancellationToken cancellationToken)
    {
        Product entity = new()
        {
            Title = request.Title,
            InventoryCount = _configuration.Value.InventoryCount,
            Price = request.Price,
            Discount = request.Discount,
            FinalPrice = CalculatePrice(request.Price, request.Discount)
        };


        _productRepository.Insert(entity);
        int res = await _productRepository.SaveChanges();

        return res;
    }

    private long CalculatePrice(long price, int discount)
    {
        long discount_amount = price * discount / 100;
        long price_after_discount = price - discount_amount;
        return price_after_discount;
    }
}