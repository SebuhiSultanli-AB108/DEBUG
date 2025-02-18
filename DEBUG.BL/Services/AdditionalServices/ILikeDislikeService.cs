using DEBUG.BL.DTOs.AdditionalDTOs;
using DEBUG.Core.Entities;
using DEBUG.Core.Enums;

namespace DEBUG.BL.Services.AdditionalServices;

public interface ILikeDislikeService
{
    Task LikeDislikeItemAsync(User user, int itemId, LikedEntityTypes entityType, bool isLiked);
    Task<LikeDislikeDTO> GetLikeDislikeCountAsync(int itemId, LikedEntityTypes entityType);
}