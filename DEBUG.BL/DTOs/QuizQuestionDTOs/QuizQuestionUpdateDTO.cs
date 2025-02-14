using DEBUG.BL.DTOs.QuizAnswerDTOs;

namespace DEBUG.BL.DTOs.QuizQuestionDTOs;

public class QuizQuestionUpdateDTO
{
    public string Content { get; set; }
    public int Difficulty { get; set; }
    public IEnumerable<QuizAnswerUpdateDTO> QuizAnswerUpdateDTOs { get; set; }
    public int TagId { get; set; }
}