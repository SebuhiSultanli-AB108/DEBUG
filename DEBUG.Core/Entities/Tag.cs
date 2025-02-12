namespace DEBUG.Core.Entities;

public class Tag : BaseEntity
{
    public string Name { get; set; }
    public IEnumerable<Question> Questions { get; set; }
}
