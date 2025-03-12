using DEBUG.BL.DTOs.AccountDTOs;
using DEBUG.Core.Entities;
using Microsoft.AspNetCore.Http;

namespace DEBUG.BL.Services.UserServices;

public interface IUserService
{
    Task<UserGetDTO> GetUserById(string id);
    Task ResetFailedLoginAttemptsAsync(string id);
    Task UnBanAsync(string id);
    Task BanAsync(string id, int banDurationWithMinutes);
    Task<IEnumerable<User>> GetAllAsync();
    string GetBadges(User user);
    Task MakeModeratorAsync(string id);
    Task<string> RegisterAsync(RegisterDTO dto);
    Task<string> LoginAsync(LoginDTO dto);
    Task LogoutAsync();
    Task SetProfileImageAsync(User user, IFormFile image);
    IEnumerable<string> GetFollowers(User user);
    IEnumerable<string> GetFollowing(User user);
    Task FollowAsync(User follower, string followingId);
    Task UnFollowAsync(User follower, string followingId);
}
