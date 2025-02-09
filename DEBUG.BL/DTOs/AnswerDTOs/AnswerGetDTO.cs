namespace DEBUG.BL.DTOs.AnswerDTOs;

public class AnswerGetDTO
{
    public int Id { get; set; }
    public string Content { get; set; }
    public int Like { get; set; }
    public int DisLike { get; set; }
    public int QuestionId { get; set; }
    public string UserName { get; set; }
    public string UserProfileImage { get; set; }
}