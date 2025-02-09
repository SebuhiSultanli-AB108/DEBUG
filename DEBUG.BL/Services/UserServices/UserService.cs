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

namespace DEBUG.BL.Services.UserServices;

public class UserService(UserManager<User> _userManager, SignInManager<User> _signInManager, IJWTTokenHandler _tokenHandler, IMapper _mapper) : IUserService
{
    readonly HttpContext _context;
    public async Task<string> RegisterAsync(RegisterDTO dto)
    {
        if (!dto.HasAcceptedTerms)
            throw new TermsAndPrivacyPolicyException();
        User newUser = _mapper.Map<User>(dto);
        newUser.Role = Roles.User.ToString();
        await _userManager.CreateAsync(newUser, dto.Password);
        User? user = await _userManager.FindByEmailAsync(dto.Email);
        //SendEmail(_tokenHandler.CreateToken(user, 1), user.Email); //TODO: Fix this shit!
        return user.Id;
    }
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _userManager.Users.ToListAsync();
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
    private void SendEmail(string token, string username)
    {
        using SmtpClient client = new SmtpClient();
        client.Host = "smtp.gmail.com";
        client.Port = 587;
        client.EnableSsl = true;
        client.UseDefaultCredentials = false;
        client.Credentials = new NetworkCredential("sabuhies-ab108@code.edu.az", "lonk jxoz dbol want");
        MailAddress from = new MailAddress("sabuhies-ab108@code.edu.az", "Blogg");
        MailAddress to = new MailAddress("sultanlisebuhi@gmail.com");
        MailMessage message = new MailMessage(from, to);
        message.Subject = "<p>Verify your email address</p>";
        string url = _context.Request.Scheme
            + "://" + _context.Request.Host
            + "/Account/VerifyEmail?token=" + token
            + "&user=" + username;
        message.IsBodyHtml = true;
        client.Send(message);
    }

}