using AutoMapper;
using DEBUG.BL.ViewModels.AnswerVMs;
using DEBUG.Core.Models;

namespace DEBUG.BL.Profiles;

public class AnswerProfile : Profile
{
    public AnswerProfile()
    {
        CreateMap<AnswerCreateVM, Answer>();
        CreateMap<AnswerGetVM, Answer>()
            .ForMember(x => x.User, opt => opt.MapFrom(x => x.UserName))
            .ReverseMap();
    }
}