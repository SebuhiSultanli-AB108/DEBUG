using Microsoft.AspNetCore.Identity;

namespace DEBUG.Core.Models;

public class User : IdentityUser
{
    public string FullName { get; set; }
    public string ProfileImage { get; set; }
    public IEnumerable<Question> Questions { get; set; }
    public IEnumerable<Answer> Answers { get; set; }
    public IEnumerable<Comment> Comments { get; set; }
}
