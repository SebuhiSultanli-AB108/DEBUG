using AutoMapper;
using DEBUG.BL.DTOs.CommentDTOs;
using DEBUG.BL.Exceptions.Common.Common;
using DEBUG.Core.Entities;
using DEBUG.Core.RepositoryInstances;
using Microsoft.AspNetCore.Identity;

namespace DEBUG.BL.Services.CommentServices;

public class CommentService(ICommentRepository _repository, UserManager<User> _userManager, IMapper _mapper) : ICommentService
{
    public async Task<int> CreateAsync(int answerId, CommentCreateDTO dto, User user)
    {
        Comment Comment = _mapper.Map<Comment>(dto);
        Comment.AnswerId = answerId;
        Comment.UserId = user.Id;
        await _repository.CreateAsync(Comment);
        await _repository.SaveChangesAsync();
        return Comment.Id;
    }
    public IEnumerable<CommentGetDTO> GetAllByAnswerId(int answerId)
    {
        return _mapper.Map<IEnumerable<CommentGetDTO>>(_repository.GetWhere(x => x.IsDeleted == false && x.AnswerId == answerId, "User"));
    }
    public IEnumerable<CommentGetDTO> GetAll()
    {
        return _mapper.Map<IEnumerable<CommentGetDTO>>(_repository.GetWhere(x => x.IsDeleted == false, "User"));
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
        return _mapper.Map<IEnumerable<CommentGetDTO>>(_repository.GetWhere(x => x.UserId == id, "User"));
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
        comment.UpdatedAt = DateTime.Now;
        _mapper.Map(dto, comment);
        await _repository.SaveChangesAsync();
        return _mapper.Map<CommentGetDTO>(comment);
    }
}
