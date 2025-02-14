﻿using AutoMapper;
using DEBUG.BL.DTOs.AccountDTOs;
using DEBUG.BL.Services.UserServices;
using DEBUG.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DEBUG.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IUserService _service, UserManager<User> _userManager, IMapper _mapper) : ControllerBase
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
    public async Task<IActionResult> SetProfileImage(IFormFile image)
    {
        _service.SetProfileImage(await _userManager.GetUserAsync(User), image);
        return Ok();
    }
    [HttpGet("[action]")]
    public async Task<IActionResult> VerifyEmail(string token, string user)
    {
        token = token.Replace(" ", "+");
        var entity = await _userManager.FindByNameAsync(user);
        //if (entity is null) 
        var result = await _userManager.ConfirmEmailAsync(entity, token);
        if (result.Succeeded)
            return Ok("salam");
        else
            return BadRequest();
        //TODO: here
    }
    [HttpPost("[action]")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await _service.LogoutAsync();
        return Ok();
    }
    //getquestions
    //getanswers
    //getcomments
}