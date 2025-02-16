using AutoMapper;
using DEBUG.BL.DTOs.AccountDTOs;
using DEBUG.Core.Entities;

namespace DEBUG.BL.Profiles;

public class AccountProfile : Profile
{
    public AccountProfile()
    {
        CreateMap<UserGetDTO, User>()
            .ReverseMap();
        CreateMap<RegisterDTO, User>();
        CreateMap<LoginDTO, User>();
    }
}