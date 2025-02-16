using DEBUG.BL.DTOs.AccountDTOs;
using DEBUG.Core.Entities;
using Microsoft.AspNetCore.Http;

namespace DEBUG.BL.Services.UserServices;

public interface IUserService
{
    Task<UserGetDTO> GetUserById(string id);
    Task<IEnumerable<User>> GetAllAsync();
    string GetBadges(User user);
    Task<string> RegisterAsync(RegisterDTO dto);
    Task<string> LoginAsync(LoginDTO dto);
    Task LogoutAsync();
    Task SetProfileImageAsync(User user, IFormFile image);
}
