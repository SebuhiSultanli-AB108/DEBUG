using DEBUG.BL.DTOs.ReportItemDTOs;
using DEBUG.BL.Exceptions.Common.Common;
using DEBUG.BL.Services.ReportServices;
using DEBUG.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DEBUG.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReportController(IReportService _service, UserManager<User> _userManager) : ControllerBase
{
    [Authorize(Roles = "Admin")]
    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllAsync(short pageNo, short take)
    {
        return Ok(await _service.GetAllAsync(pageNo, take));
    }
    [Authorize(Roles = "Admin")]
    [HttpGet("[action]")]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await _service.GetByIdAsync(id));
    }
    [Authorize(Roles = "Moderator,User")]
    [HttpPost("[action]")]
    public async Task<IActionResult> Create(ReportItemCreateDTO dto)
    {
        User? user = await _userManager.GetUserAsync(User);
        if (user == null) throw new NotFoundException<User>();
        var res = await _service.CreateAsync(dto, user);
        return Ok(res);
    }
    [Authorize(Roles = "Admin")]
    [HttpDelete("[action]")]
    public async Task<IActionResult> HardDelete(int id)
    {
        await _service.HardDeleteAsync(id);
        return Ok();
    }
}