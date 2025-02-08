using AutoMapper;
using DEBUG.BL.Services.UserServices;
using DEBUG.BL.ViewModels.AccountVMs;
using Microsoft.AspNetCore.Mvc;

namespace DEBUG.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IUserService _service, IMapper _mapper) : ControllerBase
{
    [HttpPost("[action]")]
    public async Task<IActionResult> Register(RegisterVM vm)
    {
        return Ok(await _service.RegisterAsync(vm));
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Login(LoginVM vm)
    {
        return Ok(await _service.LoginAsync(vm));
    }
    [HttpGet("[action]")]
    public async Task<IActionResult> Logout()
    {
        await _service.LogoutAsync();
        return Ok();
    }
}