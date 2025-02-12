namespace DEBUG.BL.DTOs.QuestionDTOs;

public class QuestionCreateDTO
{
    public int[] TagIds { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
}