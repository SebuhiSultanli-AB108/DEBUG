using Microsoft.AspNetCore.Identity;

namespace DEBUG.Core.Entities;

public class User : IdentityUser
{
    public string FullName { get; set; }
    public string Role { get; set; }
    public string ProfileImage { get; set; } = "~/images/default/default_profile_image.jpg";
    public int CorrectQuizAnswerCount { get; set; }
    public IEnumerable<Question> Questions { get; set; }
    public IEnumerable<Answer> Answers { get; set; }
    public IEnumerable<Comment> Comments { get; set; }
}
