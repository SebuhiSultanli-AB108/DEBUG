namespace DEBUG.BL.ViewModels.QuestionVMs;

public class QuestionGetVM
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int Like { get; set; }
    public int DisLike { get; set; }
    public string UserName { get; set; }
    public string UserImage { get; set; } = "ImageHere"; //TODO: Change this shit!
}