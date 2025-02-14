using DEBUG.BL.DTOs.QuizAnswerDTOs;

namespace DEBUG.BL.DTOs.QuizQuestionDTOs;

public class QuizQuestionGetDTO
{
    public int Id { get; set; }
    public string Content { get; set; }
    public int Difficulty { get; set; }
    public IEnumerable<QuizAnswerGetDTO> QuizAnswerGetDTOs { get; set; }
    public string TagName { get; set; }
}
