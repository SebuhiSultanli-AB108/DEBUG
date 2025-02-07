using AutoMapper;
using DEBUG.BL.ViewModels.QuestionVMs;
using DEBUG.Core.Models;

namespace DEBUG.BL.Profiles;

public class QuestionProfile : Profile
{
    public QuestionProfile()
    {
        CreateMap<QuestionCreateVM, Question>();
        CreateMap<QuestionGetVM, Question>()
            .ForMember(x => x.User, opt => opt.MapFrom(x => x.UserName))
            .ReverseMap();
    }
}