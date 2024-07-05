using MediatR;

namespace EngTech.Application.Contract.Commands.Products;

public record UpdateProductCommand : IRequest<int>
{
    public int Id { get; init; }
    public string Title { get; init; }
    public long Price { get; init; }
    public int Discount { get; init; }
    public int InventoryCount { get; init; }
}