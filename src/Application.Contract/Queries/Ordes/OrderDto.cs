using AutoMapper;
using EngTech.Application.Contract.Common;
using EngTech.Domain.Entities.Orders;

namespace EngTech.Application.Contract.Queries.Orders;

public class OrderDto
{
    public int Id { get; set; }
    public long Amount { get; set; }
    public int Count { get; set; }
    public long CreationDate { get; set; }
    public string Product { get; set; }


    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(o=>o.CreationDate.ToJsTime()))
                .ForMember(dest => dest.Product, opt => opt.MapFrom(o=>o.Product.Title));
        }
    }
}