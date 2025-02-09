using DEBUG.BL.DTOs.AccountDTOs;
using DEBUG.Core.Entities;

namespace DEBUG.BL.Services.UserServices;

public interface IUserService
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<string> RegisterAsync(RegisterDTO dto);
    Task<string> LoginAsync(LoginDTO dto);
    Task LogoutAsync();
}
