using DEBUG.BL.DTOs.TagDTOs;

namespace DEBUG.BL.DTOs.QuestionDTOs;

public class QuestionGetDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int Like { get; set; }
    public int DisLike { get; set; }
    public IEnumerable<TagGetDTO> Tags { get; set; }
    public string CategoryName { get; set; }
    public string UserName { get; set; }
    public string UserProfileImage { get; set; }
}