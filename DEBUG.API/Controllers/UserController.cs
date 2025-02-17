using AutoMapper;
using DEBUG.BL.DTOs.AccountDTOs;
using DEBUG.BL.Exceptions.Common.Common;
using DEBUG.BL.Services.UserServices;
using DEBUG.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DEBUG.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IUserService _service, UserManager<User> _userManager, IMapper _mapper) : ControllerBase
{
    [HttpGet("[action]")]
    public async Task<IActionResult> GetById(string id)
    {
        return Ok(await _service.GetUserById(id));
    }
    [HttpGet("[action]")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }
    [HttpPost("[action]")]
    public async Task<IActionResult> Follow(string followUserId)
    {
        User? user = await _userManager.GetUserAsync(User);
        User currentUser = await _getUserByIdAsync(user.Id);
        await _service.FollowAsync(currentUser, followUserId);
        return Ok();
    }
    [HttpPost("[action]")]
    public async Task<IActionResult> UnFollow(string unFollowUserId)
    {
        User? user = await _userManager.GetUserAsync(User);
        User currentUser = await _getUserByIdAsync(user.Id);
        await _service.UnFollowAsync(currentUser, unFollowUserId);
        return Ok();
    }
    [HttpGet("[action]")]
    public async Task<IActionResult> FollowersAndFollowingsIds(string userId)
    {
        User user = await _getUserByIdAsync(userId);
        IEnumerable<string> followers = await _service.GetFollowersAsync(user);
        IEnumerable<string> following = await _service.GetFollowingAsync(user);
        return Ok(new { Followers = followers, Followings = following });
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Register(RegisterDTO dto)
    {
        return Ok(await _service.RegisterAsync(dto));
    }
    [HttpPost("[action]")]
    public async Task<IActionResult> Login(LoginDTO dto)
    {
        return Ok(await _service.LoginAsync(dto));
    }
    [HttpPost("[action]")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await _service.LogoutAsync();
        return Ok();
    }
    [HttpPost("[action]")]
    public async Task<IActionResult> SetProfileImage(IFormFile image)
    {
        User? user = await _userManager.GetUserAsync(User);
        if (user == null) throw new NotFoundException<User>();
        await _service.SetProfileImageAsync(user, image);
        return Ok();
    }
    [HttpGet("[action]")]
    public async Task<IActionResult> GetBadges()
    {
        User? user = await _userManager.GetUserAsync(User);
        if (user == null) throw new NotFoundException<User>();
        return Ok(new { BadgeList = _service.GetBadges(user) });
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> VerifyEmail(string token, string user)
    {
        token = token.Replace(" ", "+");
        var entity = await _userManager.FindByNameAsync(user);
        if (entity is null) throw new NotFoundException<User>();
        var result = await _userManager.ConfirmEmailAsync(entity, token);
        if (result.Succeeded)
            return Ok("your email has been confirmed!");
        else
            return BadRequest();
    }
    [HttpPost("[action]")]
    public async Task<IActionResult> Ban(string userId, int banDuration)
    {
        await _service.BanAsync(userId, banDuration);
        return Ok();
    }
    [HttpPost("[action]")]
    public async Task<IActionResult> Unban(string userId)
    {
        await _service.UnBanAsync(userId);
        return Ok();
    }
    [HttpPost("[action]")]
    public async Task<IActionResult> ResetFailedLoginAttempts(string userId)
    {
        await _service.ResetFailedLoginAttemptsAsync(userId);
        return Ok();
    }
    async Task<User> _getUserByIdAsync(string userId)
    {
        User? user = await _userManager.Users
            .Include(x => x.Followings)
            .Include(x => x.Followers)
            .FirstOrDefaultAsync(x => x.Id == userId);
        if (user == null) throw new NotFoundException<User>();
        return user;
    }
    //getquestions
    //getanswers
    //getcomments
}