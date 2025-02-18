using DEBUG.BL.DTOs.AdditionalDTOs;
using DEBUG.Core.Entities;
using DEBUG.Core.Enums;
using DEBUG.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace DEBUG.BL.Services.AdditionalServices;

public class LikeDislikeService(AppDbContext _context) : ILikeDislikeService
{
    public async Task LikeDislikeItemAsync(User user, int itemId, LikedEntityTypes entityType, bool isLiked)
    {
        LikeDislike likeDislike = new()
        {
            User = user,
            ItemId = itemId,
            IsLiked = isLiked,
            Type = entityType
        };
        await _context.AddAsync(likeDislike);
        await _context.SaveChangesAsync();
    }
    public async Task<LikeDislikeDTO> GetLikeDislikeCountAsync(int itemId, LikedEntityTypes entityType)
    {
        var table = _context.LikeDislikes.Where(x => x.Type == entityType && x.ItemId == itemId);
        int likes = await table.Where(x => x.IsLiked == true).CountAsync();
        int disLikes = await table.Where(x => x.IsLiked == false).CountAsync();
        return new LikeDislikeDTO { Like = likes, Dislike = disLikes };
    }
}
