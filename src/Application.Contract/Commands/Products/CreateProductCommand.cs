using MediatR;

namespace EngTech.Application.Contract.Commands.Products;

public record CreateProductCommand : IRequest<int>
{
    public string Title { get; init; }
    public long Price { get; init; }
    public int Discount { get; init; }
}