using MediatR;

namespace EngTech.Application.Contract.Queries.Products;

public record GetProductByIdQuery : IRequest<GetProductByIdDto>
{
    public int Id { get; init; }
}