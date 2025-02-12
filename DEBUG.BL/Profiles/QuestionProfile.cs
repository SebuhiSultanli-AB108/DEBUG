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
        CreateMap<Question, QuestionGetDTO>()
            .ForMember(x => x.UserName, opt => opt.MapFrom(x => x.User.UserName))
            .ForMember(x => x.UserProfileImage, opt => opt.MapFrom(x => x.User.ProfileImage))
            .ForMember(x => x.CategoryName, opt => opt.MapFrom(x => x.Category.Name))
            .ForMember(x => x.Tags, opt => opt.MapFrom(x => x.Tags))
            .ReverseMap();
    }
}