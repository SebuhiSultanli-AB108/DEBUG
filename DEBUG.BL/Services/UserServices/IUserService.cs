﻿using DEBUG.BL.DTOs.AccountDTOs;
using DEBUG.Core.Entities;
using Microsoft.AspNetCore.Http;

namespace DEBUG.BL.Services.UserServices;

public interface IUserService
{
    Task<IEnumerable<User>> GetAllAsync();
    IEnumerable<string> GetBadges(User user);
    Task<string> RegisterAsync(RegisterDTO dto);
    Task<string> LoginAsync(LoginDTO dto);
    Task LogoutAsync();
    Task SetProfileImageAsync(User user, IFormFile image);
}
