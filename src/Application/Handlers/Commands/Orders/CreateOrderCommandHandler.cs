using EngTech.Application.Contract.Commands.Orders;
using EngTech.Application.Contract.Common.Models;
using EngTech.Domain.Entities.Orders;
using EngTech.Domain.Entities.Products;
using MediatR;
using Microsoft.Extensions.Options;

namespace EngTech.Application.Handlers.Commands.Orders;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
{
    private readonly IOptions<AppSettings> _configuration;
    private readonly IOrderRepository _OrderRepository;
    private readonly IProductRepository _productRepository;

    public CreateOrderCommandHandler(IOrderRepository OrderRepository,
        IOptions<AppSettings> configuration, IProductRepository productRepository)
    {
        _OrderRepository = OrderRepository;
        _configuration = configuration;
        _productRepository = productRepository;
    }

    public async Task<int> Handle(CreateOrderCommand request,
        CancellationToken cancellationToken)
    {
        if (CheckInvetoryProduct(request.ProductId, request.Count))
        {
            Product foundProduct =
                _productRepository.GetBy(p => p.Id == request.ProductId).Single();
            Order entity = new()
            {
                ProductId = request.ProductId,
                UserId = request.UserId,
                Count = request.Count,
                Amount = request.Count * foundProduct.FinalPrice,
                CreationDate = DateTime.UtcNow,
                CreatedBy = request.UserId
            };
            _OrderRepository.Insert(entity);

            DecreaseInventoryCount(request.ProductId, request.Count);
            int res = await _OrderRepository.SaveChanges();
            return res;
        }

        throw new Exception("Product not exist");
    }

    private bool CheckInvetoryProduct(int productId, int count)
    {
        bool exist = _productRepository.CheckInvetoryProduct(productId, count);
        return exist;
    }

    private void DecreaseInventoryCount(int productId, int count)
    {
        Product found = _productRepository.GetBy(p => p.Id == productId).Single();
        found.InventoryCount -= count;
        _productRepository.Update(found);
    }
}