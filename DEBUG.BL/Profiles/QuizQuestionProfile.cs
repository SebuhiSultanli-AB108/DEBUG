using AutoMapper;
using DEBUG.BL.DTOs.QuizQuestionDTOs;
using DEBUG.Core.Entities;

namespace DEBUG.BL.Profiles;

public class QuizQuestionProfile : Profile
{
    public QuizQuestionProfile()
    {
        CreateMap<QuizQuestionCreateDTO, QuizQuestion>()
            .ForMember(x => x.QuizAnswers, opt => opt.MapFrom(x => x.QuizAnswerCreateDTOs))
            .ReverseMap();
        CreateMap<QuizQuestionUpdateDTO, QuizQuestion>()
            .ForMember(x => x.QuizAnswers, opt => opt.MapFrom(x => x.QuizAnswerUpdateDTOs))
            .ReverseMap();
        CreateMap<QuizQuestion, QuizQuestionGetDTO>()
            .ForMember(x => x.QuizAnswerGetDTOs, opt => opt.MapFrom(x => x.QuizAnswers))
            .ForMember(x => x.TagName, opt => opt.MapFrom(x => x.Tag.Name));
    }
}