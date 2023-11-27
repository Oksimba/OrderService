using AutoMapper;
using Entities;

namespace OrderAPI.Profiles
{
    public class OrderProfile: Profile
    {
        public OrderProfile() 
        {
            CreateMap<OrderCreateDto, Order>();
            CreateMap<OrderUpdateDto, Order>();
            CreateMap<Order?, OrderDto>();
        }
    }
}
