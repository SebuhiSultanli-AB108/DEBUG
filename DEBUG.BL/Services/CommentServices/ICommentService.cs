using DEBUG.BL.DTOs.CommentDTOs;
using DEBUG.Core.Entities;

namespace DEBUG.BL.Services.CommentServices;

public interface ICommentService
{
    Task<int> CreateAsync(int answerId, CommentCreateDTO dto, User user);
    Task<IEnumerable<CommentGetDTO>> GetAllByAnswerIdAsync(int answerId);
    Task<CommentGetDTO> UpdateAsync(int id, CommentUpdateDTO dto);
    Task HardDeleteAsync(int id);
    Task SoftDeleteOrRestoreAsync(int id);
    Task<IEnumerable<CommentGetDTO>> GetAllAsync();
    Task<IEnumerable<CommentGetDTO>> GetByUserIdAsync(string id);
    Task<CommentGetDTO> GetByIdAsync(int id);
}
