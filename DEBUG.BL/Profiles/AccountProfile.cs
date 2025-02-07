using AutoMapper;
using DEBUG.BL.ViewModels.AccountVMs;
using DEBUG.Core.Models;

namespace DEBUG.BL.Profiles;

public class AccountProfile : Profile
{
    public AccountProfile()
    {
        CreateMap<RegisterVM, User>();
        CreateMap<LoginVM, User>();
        CreateMap<UserGetVM, User>()
            .ReverseMap();
    }
}