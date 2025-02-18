using DEBUG.Core.Enums;

namespace DEBUG.Core.Entities;

public class LikeDislike
{
    public int Id { get; set; }
    public bool IsLiked { get; set; }
    public int ItemId { get; set; }
    public LikedEntityTypes Type { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}
