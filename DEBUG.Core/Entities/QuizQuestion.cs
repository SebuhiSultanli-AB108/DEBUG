namespace DEBUG.Core.Entities;

public class QuizQuestion : BaseEntity
{
    public string Content { get; set; }
    public int Difficulty { get; set; }
    public IEnumerable<QuizAnswer> QuizAnswers { get; set; }
    public int TagId { get; set; }
    public Tag Tag { get; set; }
}