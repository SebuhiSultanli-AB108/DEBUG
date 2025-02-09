using AutoMapper;
using DEBUG.BL.DTOs.CommentDTOs;
using DEBUG.Core.Entities;

namespace DEBUG.BL.Profiles;

public class CommentProfile : Profile
{
    public CommentProfile()
    {
        CreateMap<CommentCreateDTO, Comment>();
        CreateMap<CommentUpdateDTO, Comment>()
            .ReverseMap();
        CreateMap<CommentGetDTO, Comment>()
            .ForMember(x => x.User, opt => opt.MapFrom(x => x.UserName))
            .ReverseMap();
    }
}