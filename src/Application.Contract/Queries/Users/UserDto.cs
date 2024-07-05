using AutoMapper;
using EngTech.Application.Contract.Queries.Orders;
using EngTech.Domain.Entities.Users;

namespace EngTech.Application.Contract.Queries.Users;

public class UserDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<OrderDto> Orders { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<User, UserDto>();
            
        }
    }
}