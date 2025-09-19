using AutoMapper;
using DAL.Models;
using DTO.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.MapperProfiles
{
    public class OrderProfile: Profile
    {

        public OrderProfile()
        {
            #region read
            CreateMap<Orders, OrderResponseDto>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer != null ? src.Customer.Name : "Unknown Customer"))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.OrderItems.Sum(item => item.UnitPrice * item.Quantity)))
                .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems))
                .ForMember(d => d.Stauts, opt => opt.MapFrom(src => src.Status.ToString())).ReverseMap();



            CreateMap<OrderItem, OrderItemsRepsonseDto>()
                .ForMember(dest => dest.productName, opt => opt.MapFrom(src => src.Product != null ? src.Product.Name : "Unknown Product"))
                .ForMember(dest => dest.totalPrice, opt => opt.MapFrom(src => src.UnitPrice * src.Quantity))
                .ForMember(dest => dest.price, opt => opt.MapFrom(src => src.UnitPrice))
                .ForMember(dest => dest.productId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.orderItemId, opt => opt.MapFrom(src => src.OrderItemId))
                .ReverseMap();
            #endregion

            #region create 

            CreateMap<CreateOrderDto, Orders>()
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => OrderStatus.Pending))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.OrderItems.Sum(item => item.UnitPrice * item.quantity)))
                .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems))
                .ReverseMap();

            CreateMap<CreateOrderItemDto, OrderItem>()
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.quantity))
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.productId));
            #endregion
        }


    }

}
