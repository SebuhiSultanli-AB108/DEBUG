namespace DEBUG.Core.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; }
    public IEnumerable<Question> Questions { get; set; }
}