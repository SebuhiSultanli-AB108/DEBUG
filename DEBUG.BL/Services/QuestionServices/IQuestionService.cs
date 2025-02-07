using DEBUG.BL.ViewModels.QuestionVMs;
using DEBUG.Core.Models;

namespace DEBUG.BL.Services.QuestionServices;

public interface IQuestionService
{
    Task CreateAsync(QuestionCreateVM vm, User user);
    Task UpdateAsync(int id, QuestionCreateVM vm);
    Task HardDeleteAsync(int id);
    Task SoftDeleteAndRestore(int id);
    IEnumerable<QuestionGetVM> GetAll();
    IEnumerable<QuestionGetVM> GetByUserId(string id);
    Task<QuestionGetVM> GetByIdAsync(int id);
}
