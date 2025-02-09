namespace DEBUG.BL.DTOs.QuestionDTOs;

public class QuestionGetDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int Like { get; set; }
    public int DisLike { get; set; }
    public string UserName { get; set; }
    public string UserProfileImage { get; set; }
}