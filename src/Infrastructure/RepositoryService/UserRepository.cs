using EngTech.Domain.Entities.Users;
using EngTech.Infrastructure.Common;
using EngTech.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EngTech.Infrastructure.RepositoryService;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    private readonly DbSet<User> _entity;

    public UserRepository(ApplicationDbContext context) : base(context)
    {
        _entity = context.Set<User>();
    }
}