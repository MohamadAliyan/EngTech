using MediatR;

namespace EngTech.Application.Contract.Queries.Users;

public record GetAllUserQuery : IRequest<List<UserDto>>
{
}