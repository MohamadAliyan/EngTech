using AutoMapper;
using AutoMapper.QueryableExtensions;
using EngTech.Application.Contract.Queries.Users;
using EngTech.Domain.Entities.Users;
using EngTech.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace EngTech.Application.Handlers.Queries.Users;

public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, List<UserDto>>
{
    private readonly IMapper _mapper;
    private readonly IMemoryCache _memoryCache;
    private readonly IUserRepository _userRepository;

    public GetAllUserQueryHandler(IUserRepository userRepository,
        IMemoryCache memoryCache,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _memoryCache = memoryCache;
        _mapper = mapper;
    }

    public async Task<List<UserDto>> Handle(GetAllUserQuery request,
        CancellationToken cancellationToken)
    {
        string cacheKey = Constant.GetAllUsers;
        List<UserDto> UsersCache;
        if (_memoryCache.TryGetValue(cacheKey, out UsersCache))
        {
            return UsersCache;
        }

       

        List<UserDto> users = _userRepository
            .GetAll()
            .Include(p=>p.Orders).ThenInclude(p=>p.Product)
            .AsNoTracking()
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider).ToList();


        MemoryCacheEntryOptions cacheEntryOptions =
            new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(1));

        _memoryCache.Set(cacheKey, users, cacheEntryOptions);
        return users;
    }
}