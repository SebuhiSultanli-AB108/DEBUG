using AutoMapper;
using DEBUG.BL.DTOs.CategoryDTOs;
using DEBUG.Core.Entities;

namespace DEBUG.BL.Profiles;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<CategoryCreateDTO, Category>();
        CreateMap<CategoryUpdateDTO, Category>()
            .ReverseMap();
        CreateMap<CategoryGetDTO, Category>()
            .ReverseMap();
    }
}