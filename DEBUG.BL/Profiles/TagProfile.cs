using AutoMapper;
using DEBUG.BL.DTOs.TagDTOs;
using DEBUG.Core.Entities;

namespace DEBUG.BL.Profiles;

public class TagProfile : Profile
{
    public TagProfile()
    {
        CreateMap<TagCreateDTO, Tag>();
        CreateMap<TagUpdateDTO, Tag>()
            .ReverseMap();
        CreateMap<TagGetDTO, Tag>()
            .ReverseMap();
    }
}