namespace DEBUG.Core.Entities;

public class Question : BaseEntity
{
    public string Title { get; set; }
    public string Content { get; set; }
    public int Like { get; set; }
    public int DisLike { get; set; }
    public IEnumerable<Answer> Answers { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public IEnumerable<Tag> Tags { get; set; }

}