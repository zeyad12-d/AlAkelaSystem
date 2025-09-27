using AutoMapper;
using DAL.Models;
using DTO.CouponDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.MapperProfiles
{
    public sealed class CouponesProfile: Profile
    {
        public CouponesProfile()
        {

            CreateMap<Coupon, CouponResponseDto>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Code, o => o.MapFrom(s => s.Code))
                .ForMember(d => d.DiscountAmount, o => o.MapFrom(s => s.DiscountAmount))
                .ForMember(d => d.ExpiryDate, o => o.MapFrom(s => s.ExpiryDate))
                .ForMember(d => d.IsActive, o => o.MapFrom(s => s.IsActive)).ReverseMap();

            CreateMap <Coupon, CreateCouponDto>()
                .ForMember(d => d.Code, o => o.MapFrom(s => s.Code))
                .ForMember(d => d.DiscountAmount, o => o.MapFrom(s => s.DiscountAmount))
                .ForMember(d => d.ExpiryDate, o => o.MapFrom(s => s.ExpiryDate))
                .ForMember(d => d.IsActive, o => o.MapFrom(s => s.IsActive)).ReverseMap();

            CreateMap<Coupon, UpdateCouponDto>()
        .ForMember(d => d.Code, o => o.MapFrom(s => s.Code))
                .ForMember(d => d.DiscountAmount, o => o.MapFrom(s => s.DiscountAmount))
                .ForMember(d => d.ExpiryDate, o => o.MapFrom(s => s.ExpiryDate))
                .ForMember(d => d.IsActive, o => o.MapFrom(s => s.IsActive)).ReverseMap();


        }
    }
}
