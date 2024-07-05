using EngTech.Application.Contract.Commands.Users;
using EngTech.Application.Contract.Common.Models;
using EngTech.Domain.Entities.Users;
using MediatR;
using Microsoft.Extensions.Options;

namespace EngTech.Application.Handlers.Commands.Users;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
{
    private readonly IOptions<AppSettings> _configuration;
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<int> Handle(CreateUserCommand request,
        CancellationToken cancellationToken)
    {
        _userRepository.Insert(new User { Name = request.Name });
        int res = await _userRepository.SaveChanges();
        return res;
    }
}