using DEBUG.BL.ViewModels.AnswerVMs;
using DEBUG.BL.ViewModels.CommentVMs;
using DEBUG.BL.ViewModels.QuestionVMs;

namespace DEBUG.BL.ViewModels.CombinedVMs;

public class QuestionAnswerGetVM
{
    public QuestionGetVM Question { get; set; }
    public AnswerCreateVM Answer { get; set; }
    public CommentCreateVM Comment { get; set; }
    public IEnumerable<AnswerGetVM> Answers { get; set; }
}