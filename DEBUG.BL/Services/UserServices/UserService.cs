using DEBUG.BL.Exceptions.Common.Common;
using Microsoft.AspNetCore.Identity;
using System.Net.Mail;
using System.Net;
using AutoMapper;
using DEBUG.BL.ExternalServices;
using Microsoft.AspNetCore.Http;
using DEBUG.BL.DTOs.AccountDTOs;
using DEBUG.Core.Enums;
using DEBUG.BL.Exceptions.UserExceptions;
using Microsoft.EntityFrameworkCore;
using DEBUG.Core.Entities;
using DEBUG.BL.Helpers.EmailTemplates;
using DEBUG.BL.Extensions;
using Microsoft.AspNetCore.Hosting;
using DEBUG.DAL.Context;

namespace DEBUG.BL.Services.UserServices;

public class UserService(
    UserManager<User> _userManager,
    SignInManager<User> _signInManager,
    AppDbContext _dbContext,
    IJWTTokenHandler _tokenHandler,
    IMapper _mapper,
    IHttpContextAccessor accessor,
    IWebHostEnvironment _wwwRoot) : IUserService
{
    readonly HttpContext _context = accessor.HttpContext;
    public async Task<IEnumerable<string>> GetFollowersAsync(User user)
    {
        return user.Followers.Select(x => x.Id);
    }
    public async Task<IEnumerable<string>> GetFollowingAsync(User user)
    {
        return user.Followings.Select(x => x.Id);
    }
    public async Task FollowAsync(User follower, string followingId)
    {
        User? following = await _userManager.FindByIdAsync(followingId);
        if (following == null) throw new NotFoundException<User>();
        if (follower.Followings.Contains(following)) throw new AlreadyFollowingException();
        follower.Followings.Add(following);
        follower.FollowingCount++;
        following.Followers.Add(follower);
        following.FollowerCount++;
        await _dbContext.SaveChangesAsync();
    }
    public async Task UnFollowAsync(User follower, string followingId)
    {
        User? following = await _userManager.FindByIdAsync(followingId);
        if (following == null) throw new NotFoundException<User>();
        if (follower.Followings.Contains(following)) throw new AlreadyFollowingException();
        follower.Followings.Remove(following);
        follower.FollowingCount--;
        following.Followers.Remove(follower);
        following.FollowerCount--;
        await _dbContext.SaveChangesAsync();
    }
    public async Task<string> RegisterAsync(RegisterDTO dto)
    {
        if (!dto.HasAcceptedTerms)
            throw new TermsAndPrivacyPolicyException();
        User newUser = _mapper.Map<User>(dto);
        newUser.Role = Roles.User.ToString();
        await _userManager.CreateAsync(newUser, dto.Password);
        User? user = await _userManager.FindByEmailAsync(dto.Email);
        SendEmail(await _userManager.GenerateEmailConfirmationTokenAsync(user), user.Email, user.UserName);
        return user.Id;
    }
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _userManager.Users.ToListAsync();
    }
    public async Task<UserGetDTO> GetUserById(string id)
    {
        User? user = await _userManager.FindByIdAsync(id);
        if (user == null) throw new NotFoundException<User>();
        return _mapper.Map<UserGetDTO>(user);
    }
    public string GetBadges(User user)
    {
        return string.Join(", ", Enum.GetValues(typeof(Badges))
            .Cast<Badges>()
            .Where(badge => (user.Badges & badge) == badge));
    }
    public async Task<string> LoginAsync(LoginDTO dto)
    {
        User? user = await _userManager.FindByEmailAsync(dto.Email);
        if (user is null) throw new NotFoundException<User>();

        var res = await _signInManager.PasswordSignInAsync(user!, dto.Password, true, true);
        var passwordHasher = new PasswordHasher<object>();
        if (passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password) == PasswordVerificationResult.Failed)
            throw new NotFoundException<User>();
        return _tokenHandler.CreateToken(user, 24);
    }
    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }
    public async Task BanAsync(string id, int banDurationWithMinutes)
    {
        User? user = await _userManager.FindByIdAsync(id);
        if (user is null) throw new NotFoundException<User>();
        await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow.AddMinutes(banDurationWithMinutes));
    }
    public async Task UnBanAsync(string id)
    {
        User? user = await _userManager.FindByIdAsync(id);
        if (user is null) throw new NotFoundException<User>();
        await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow);
    }
    public async Task ResetFailedLoginAttemptsAsync(string id)
    {
        User? user = await _userManager.FindByIdAsync(id);
        if (user is null) throw new NotFoundException<User>();
        await _userManager.ResetAccessFailedCountAsync(user);
    }
    public async Task SetProfileImageAsync(User user, IFormFile image)
    {
        if (user is null) throw new NotFoundException<User>();
        user.ProfileImage = await image.UploadAsync(_wwwRoot, "user", user.UserName);
        await _dbContext.SaveChangesAsync();
    }
    private void SendEmail(string token, string email, string username)
    {
        using SmtpClient client = new SmtpClient();
        client.Host = "smtp.gmail.com";
        client.Port = 587;
        client.EnableSsl = true;
        client.UseDefaultCredentials = false;
        client.Credentials = new NetworkCredential("debug.app.noreply@gmail.com", "qlfm vdmt dbka ngnl");
        MailAddress from = new MailAddress("debug.app.noreply@gmail.com", "DEBUG");
        MailAddress to = new MailAddress(email);
        MailMessage message = new MailMessage(from, to);
        message.Subject = "<p>Verify your email address</p>";
        string url = _context.Request.Scheme
            + "://" + _context.Request.Host
            + "/api/User/VerifyEmail?token=" + token
            + "&user=" + username;
        message.Body = VerifyEmailTemplate.VerifyEmail.Replace("__$name", username).Replace("__$link", url);
        message.IsBodyHtml = true;
        client.Send(message);
    }

}