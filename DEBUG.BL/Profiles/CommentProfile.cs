using AutoMapper;
using DEBUG.BL.ViewModels.CommentVMs;
using DEBUG.Core.Models;

namespace DEBUG.BL.Profiles;

public class CommentProfile : Profile
{
    public CommentProfile()
    {
        CreateMap<CommentCreateVM, Comment>();
        CreateMap<CommentGetVM, Comment>().ReverseMap();
    }
}