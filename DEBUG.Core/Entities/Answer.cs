namespace DEBUG.Core.Entities;

public class Answer : BaseEntity
{
    public string Content { get; set; }
    public int Like { get; set; }
    public int DisLike { get; set; }
    public IEnumerable<Comment> Comments { get; set; }
    public int QuestionId { get; set; }
    public Question Question { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
}