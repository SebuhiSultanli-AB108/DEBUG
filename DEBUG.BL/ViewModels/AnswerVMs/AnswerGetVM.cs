using DEBUG.BL.ViewModels.CommentVMs;

namespace DEBUG.BL.ViewModels.AnswerVMs;

public class AnswerGetVM
{
    public int Id { get; set; }
    public string Content { get; set; }
    public int Like { get; set; }
    public int DisLike { get; set; }
    public string UserName { get; set; }
    public string UserImage { get; set; } = "ImageHere"; //TODO: Change this shit!
    public IEnumerable<CommentGetVM> Comments { get; set; }
}