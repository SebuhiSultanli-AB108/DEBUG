using AutoMapper;
using DEBUG.BL.DTOs.QuizAnswerDTOs;
using DEBUG.Core.Entities;

namespace DEBUG.BL.Profiles;

public class QuizAnswerProfile : Profile
{
    public QuizAnswerProfile()
    {
        CreateMap<QuizAnswerCreateDTO, QuizAnswer>()
            .ReverseMap();
        CreateMap<QuizAnswerUpdateDTO, QuizAnswer>()
            .ReverseMap();
        CreateMap<QuizAnswerGetDTO, QuizAnswer>()
            .ReverseMap();
    }
}