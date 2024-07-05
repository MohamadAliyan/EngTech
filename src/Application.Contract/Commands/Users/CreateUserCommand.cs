using MediatR;

namespace EngTech.Application.Contract.Commands.Users;

public record CreateUserCommand : IRequest<int>
{
    public string Name { get; set; }
}