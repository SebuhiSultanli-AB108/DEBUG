using DEBUG.BL.DTOs.QuizAnswerDTOs;
namespace DEBUG.BL.DTOs.QuizQuestionDTOs;

public class QuizQuestionCreateDTO
{
    public string Content { get; set; }
    public int Difficulty { get; set; }
    public IEnumerable<QuizAnswerCreateDTO> QuizAnswerCreateDTOs { get; set; }
    public int TagId { get; set; }
}