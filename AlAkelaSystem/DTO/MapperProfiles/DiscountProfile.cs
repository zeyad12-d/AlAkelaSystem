using AutoMapper;
using DAL.Models;
using DTO.DiscountDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.MapperProfiles
{
    public sealed class DiscountProfile :Profile
    {
        public DiscountProfile()
        {
            #region read

            CreateMap<Discount, DiscountResponseDto>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.discountName, opt => opt.MapFrom(src => src.DiscountName))
                .ForMember(dest => dest.discountValue, opt => opt.MapFrom(src => src.DiscountValue))
                .ReverseMap();
            #endregion

            #region create
            CreateMap<CreateDiscountDto, Discount>()
                .ForMember(dest => dest.DiscountName, opt => opt.MapFrom(src => src.discountName))
                .ForMember(dest => dest.DiscountValue, opt => opt.MapFrom(src => src.discountValue));

            #endregion

            #region update
            CreateMap<UpdateDiscountDto, Discount>()
                .ForMember(D => D.Id, op => op.MapFrom(s => s.id))
                .ForMember(D => D.DiscountName, op => op.MapFrom(s => s.discountName))
                .ForMember(D => D.DiscountValue, op => op.MapFrom(s => s.discountValue));
            #endregion
        }
    }
}
