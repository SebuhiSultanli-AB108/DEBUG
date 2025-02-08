using DEBUG.BL.Exceptions.Common.Common;
using DEBUG.BL.ViewModels.AccountVMs;
using DEBUG.Core.Models;
using Microsoft.AspNetCore.Identity;
using System.Net.Mail;
using System.Net;
using AutoMapper;
using DEBUG.BL.ExternalServices;
using Microsoft.AspNetCore.Http;

namespace DEBUG.BL.Services.UserServices;

public class UserService(UserManager<User> _userManager, SignInManager<User> _signInManager, IJWTTokenHandler _tokenHandler, IMapper _mapper) : IUserService
{
    readonly HttpContext _context;
    public async Task<string> RegisterAsync(RegisterVM vm)
    {
        await _userManager.CreateAsync(_mapper.Map<User>(vm), vm.Password);
        User? user = await _userManager.FindByEmailAsync(vm.Email);
        SendEmail(_tokenHandler.CreateToken(user, 1), user.Email);
        return user.Id;
    }
    public async Task<string> LoginAsync(LoginVM vm)
    {
        User? user = await _userManager.FindByEmailAsync(vm.Email);
        if (user is null) throw new NotFoundException<User>();

        var res = await _signInManager.PasswordSignInAsync(user!, vm.Password, true, true);
        var passwordHasher = new PasswordHasher<object>();
        if (passwordHasher.VerifyHashedPassword(user, user.PasswordHash, vm.Password) == PasswordVerificationResult.Failed)
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