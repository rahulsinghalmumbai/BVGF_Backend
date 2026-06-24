using AutoMapper;
using BVGF.Entities;
using BVGFEntities.DTOs;

namespace BVGF.Mapper
{
    public class Automapperr : Profile
    {
        public Automapperr()
        {
            CreateMap<MstCategoryDto, MstCategory>().ReverseMap();
        }
    }
}
