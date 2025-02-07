using DEBUG.BL.ViewModels.AnswerVMs;
using DEBUG.Core.Models;

namespace DEBUG.BL.Services.AnswerServices;

public interface IAnswerService
{
    Task CreateAsync(AnswerCreateVM vm, int questionId, User user);
    Task UpdateAsync(int id, AnswerCreateVM vm);
    Task HardDeleteAsync(int id);
    Task SoftDeleteAndRestore(int id);
    IEnumerable<AnswerGetVM> GetAll();
    IEnumerable<AnswerGetVM> GetByQuestionId(int id);
    Task<AnswerGetVM> GetByIdAsync(int id);
}
