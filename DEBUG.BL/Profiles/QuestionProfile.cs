using AutoMapper;
using DEBUG.BL.DTOs.QuestionDTOs;
using DEBUG.Core.Entities;

namespace DEBUG.BL.Profiles;

public class QuestionProfile : Profile
{
    public QuestionProfile()
    {
        CreateMap<QuestionCreateDTO, Question>();
        CreateMap<QuestionUpdateDTO, Question>()
            .ReverseMap();
        CreateMap<QuestionGetDTO, Question>()
            .ForMember(x => x.User, opt => opt.MapFrom(x => x.UserName))
            .ReverseMap();
    }
}