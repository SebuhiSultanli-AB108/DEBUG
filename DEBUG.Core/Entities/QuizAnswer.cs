namespace DEBUG.Core.Entities;

public class QuizAnswer : BaseEntity
{
    public string Content { get; set; }
    public bool IsCorrect { get; set; }
    public int QuizQuestionId { get; set; }
    public QuizQuestion QuizQuestion { get; set; }
}