using MediatR;

namespace EngTech.Application.Contract.Commands.Orders;

public record CreateOrderCommand : IRequest<int>
{
    public int ProductId { get; init; }
    public int UserId { get; init; }
    public int Count { get; init; }
}