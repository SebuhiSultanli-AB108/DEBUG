using AutoMapper;
using DEBUG.BL.DTOs.AdditionalDTOs;
using DEBUG.BL.DTOs.CommentDTOs;
using DEBUG.BL.Exceptions;
using DEBUG.BL.Exceptions.Common.Common;
using DEBUG.BL.Services.AdditionalServices;
using DEBUG.Core.Entities;
using DEBUG.Core.Enums;
using DEBUG.Core.RepositoryInstances;
using Microsoft.AspNetCore.Identity;

namespace DEBUG.BL.Services.CommentServices;

public class CommentService(ICommentRepository _repository, UserManager<User> _userManager, ILikeDislikeService _likeService, IMapper _mapper) : ICommentService
{
    public async Task<int> CreateAsync(int answerId, CommentCreateDTO dto, User user)
    {
        Comment Comment = _mapper.Map<Comment>(dto);
        Comment.AnswerId = answerId;
        Comment.UserId = user.Id;
        await _repository.CreateAsync(Comment);

        user.CommentsCount++;
        if (user.CommentsCount >= 50)
        {
            user.Badges |= Badges.Comment50;
            user.Badges &= ~Badges.Comment25;
        }
        else if (user.CommentsCount >= 25)
            user.Badges |= Badges.Comment25;

        await _repository.SaveChangesAsync();
        return Comment.Id;
    }
    public async Task<IEnumerable<CommentGetDTO>> GetAllByAnswerIdAsync(int answerId, short pageNo, short take)
    {
        if (pageNo <= 0 || take <= 0)
            throw new PageOrTakeCantBeZeroException();
        return _mapper.Map<IEnumerable<CommentGetDTO>>(await _repository.GetWhereAsync(pageNo, take, x => x.IsDeleted == false && x.AnswerId == answerId, "User"));
    }
    public async Task<IEnumerable<CommentGetDTO>> GetAllAsync(short pageNo, short take)
    {
        if (pageNo <= 0 || take <= 0)
            throw new PageOrTakeCantBeZeroException();
        return _mapper.Map<IEnumerable<CommentGetDTO>>(await _repository.GetWhereAsync(pageNo, take, x => x.IsDeleted == false, "User"));
    }
    public async Task<CommentGetDTO> GetByIdAsync(int id)
    {
        Comment? comment = await _repository.GetByIdAsync(id, x => x.IsDeleted == false, "User");
        if (comment == null) throw new NotFoundException<Comment>();
        return _mapper.Map<CommentGetDTO>(comment);
    }
    public async Task<IEnumerable<CommentGetDTO>> GetByUserIdAsync(string id)
    {
        if (await _userManager.FindByIdAsync(id) == null) throw new NotFoundException<User>();
        return _mapper.Map<IEnumerable<CommentGetDTO>>(await _repository.GetWhereAsync(0, 0, x => x.UserId == id, "User"));
    }
    public async Task HardDeleteAsync(int id)
    {
        Comment? target = await _repository.GetByIdAsync(id);
        if (target == null) throw new NotFoundException<Comment>();
        await _repository.HardDeleteAsync(target);
        await _repository.SaveChangesAsync();
    }
    public async Task SoftDeleteOrRestoreAsync(int id)
    {
        Comment? target = await _repository.GetByIdAsync(id);
        if (target == null) throw new NotFoundException<Comment>();
        _repository.SoftDeleteAndRestore(target);
        await _repository.SaveChangesAsync();
    }
    public async Task<CommentGetDTO> UpdateAsync(int id, CommentUpdateDTO dto)
    {
        Comment? comment = await _repository.GetByIdAsync(id);
        if (comment == null) throw new NotFoundException<Comment>();
        _mapper.Map(dto, comment);
        await _repository.SaveChangesAsync();
        return _mapper.Map<CommentGetDTO>(comment);
    }

    public async Task LikeDislikeAsync(User user, int commentId, bool isLiked)
        => await _likeService.LikeDislikeItemAsync(user, commentId, LikedEntityTypes.Comment, isLiked);
    public async Task<LikeDislikeDTO> GetLikeDislikeAsync(int commentId)
        => await _likeService.GetLikeDislikeCountAsync(commentId, LikedEntityTypes.Comment);
}
