using DEBUG.BL.DTOs.AnswerDTOs;
using DEBUG.BL.DTOs.CommentDTOs;
using DEBUG.BL.DTOs.QuestionDTOs;

namespace DEBUG.BL.DTOs.CombinedDTOs;

public class QuestionAnswerGetDTO
{
    public QuestionGetDTO Question { get; set; }
    public AnswerCreateDTO Answer { get; set; }
    public CommentCreateDTO Comment { get; set; }
    public IEnumerable<AnswerGetDTO> Answers { get; set; }
}