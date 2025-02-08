using DEBUG.BL.ViewModels.AccountVMs;

namespace DEBUG.BL.Services.UserServices;

public interface IUserService
{
    Task<string> RegisterAsync(RegisterVM vm);
    Task<string> LoginAsync(LoginVM vm);
    Task LogoutAsync();
}
