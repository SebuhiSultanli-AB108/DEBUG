namespace DEBUG.BL.DTOs.QuizAnswerDTOs;

public class QuizAnswerGetDTO
{
    public int Id { get; set; }
    public string Content { get; set; }
    public bool IsCorrect { get; set; }
}