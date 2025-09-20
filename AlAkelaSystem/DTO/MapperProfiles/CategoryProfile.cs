using AutoMapper;
using DAL.Models;
using DTO.CategoryDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.MapperProfiles
{
    public sealed class CategoryProfile :Profile
    {

        public CategoryProfile()
        {
            CreateMap<Category, CategoryResponseDto>().ReverseMap();

            CreateMap<Category, CategoryDetailsDto>()
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products)).ReverseMap();

            CreateMap<CreateCategoryDto, Category>().ReverseMap();

            CreateMap<UpdateCategoryDto, Category>()
                .ForMember(D => D.Id, op => op.MapFrom(s => s.Id))
                .ForMember(D => D.Name, op => op.MapFrom(s => s.Name))
                .ForMember(D => D.Icon, op => op.MapFrom(s => s.Icon));
        }
    }
}
