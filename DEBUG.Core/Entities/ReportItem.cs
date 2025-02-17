namespace DEBUG.Core.Entities;

public class ReportItem : BaseEntity
{
    public string ReporterUserId { get; set; }
    public string ReportedUserId { get; set; }
    public string Content { get; set; }
    public int QuestionId { get; set; }
    public Question Question { get; set; }
    public int AnswerId { get; set; }
    public Answer Answer { get; set; }
    public int CommentId { get; set; }
    public Comment Comment { get; set; }
}