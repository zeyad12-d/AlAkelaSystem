using AutoMapper;
using DAL.Models;
using DTO.CustomerDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.MapperProfiles
{
    public class CustomerProfile: Profile
    {
        public CustomerProfile()


        {
            CreateMap<Customer, CustomerResponesDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
               .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address)).ReverseMap();

            CreateMap<Customer, CustomerDetailsDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.OrdersCount, opt => opt.MapFrom(src => src.Orders.Count));
        }
    }
}
