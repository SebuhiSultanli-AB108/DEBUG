using AutoMapper;
using DEBUG.BL.DTOs.AnswerDTOs;
using DEBUG.Core.Entities;

namespace DEBUG.BL.Profiles;

public class AnswerProfile : Profile
{
    public AnswerProfile()
    {
        CreateMap<AnswerCreateDTO, Answer>();
        CreateMap<AnswerUpdateDTO, Answer>()
            .ReverseMap();
        CreateMap<AnswerGetDTO, Answer>()
            .ForMember(x => x.User, opt => opt.MapFrom(x => x.UserName))
            .ReverseMap();
    }
}