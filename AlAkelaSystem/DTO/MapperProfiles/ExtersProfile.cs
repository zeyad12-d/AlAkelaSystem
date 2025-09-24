using AutoMapper;
using DAL.Models;
using DTO.ExtrasDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.MapperProfiles
{
    internal sealed class ExtersProfile : Profile
    {
        public ExtersProfile()
        {
            CreateMap<Extras, ExtrasResponseDto>().ReverseMap();

            
            CreateMap<CreateExtrasDto, Extras>();

            
            CreateMap<UpdateExtrasDto, Extras>();
        }
    }
}
