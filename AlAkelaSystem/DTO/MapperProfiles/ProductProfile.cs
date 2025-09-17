using AutoMapper;
using DAL.Models;
using DTO.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.MapperProfiles
{
    public class ProductProfile :Profile
    {
        public ProductProfile()
        {
            // Mapping configurations
            CreateMap<Product, ProductResponesDto>().ReverseMap();

            CreateMap<CreateProductDto, Product>().ReverseMap();

            CreateMap<UpdateProductDto, Product>().ReverseMap();
        }
    }
}
