﻿namespace DEBUG.BL.DTOs.ReportItemDTOs;

public class ReportItemGetDTO
{
    public int Id { get; set; }
    public string ReporterUserId { get; set; }
    public string ReportedUserId { get; set; }
    public string Content { get; set; }
    public int QuestionId { get; set; }
    public int AnswerId { get; set; }
    public int CommentId { get; set; }
}