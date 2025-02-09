using AutoMapper;
using DEBUG.BL.DTOs.AccountDTOs;
using DEBUG.BL.Services.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace DEBUG.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IUserService _service, IMapper _mapper) : ControllerBase
{
    [HttpGet("[action]")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
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
    public async Task<IActionResult> Logout()
    {
        await _service.LogoutAsync();
        return Ok();
    }
    //getquestions
    //getanswers
    //getcomments
}