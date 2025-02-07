namespace DEBUG.Core.Models;

public class Comment : BaseEntity
{
    public string CommentText { get; set; }
    public int AnswerId { get; set; }
    public Answer Answer { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
}