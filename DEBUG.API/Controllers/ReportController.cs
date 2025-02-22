using DEBUG.BL.DTOs.ReportItemDTOs;
using DEBUG.BL.Exceptions.Common.Common;
using DEBUG.BL.Services.ReportServices;
using DEBUG.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DEBUG.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReportController(IReportService _service, UserManager<User> _userManager) : ControllerBase
{
    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await _service.GetAllAsync());
    }
    [HttpGet("[action]")]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await _service.GetByIdAsync(id));
    }
    [HttpPost("[action]")]
    public async Task<IActionResult> Create(ReportItemCreateDTO dto)
    {
        User? user = await _userManager.GetUserAsync(User);
        if (user == null) throw new NotFoundException<User>();
        var res = await _service.CreateAsync(dto, user);
        return Ok(res);
    }
    [HttpDelete("[action]")]
    public async Task<IActionResult> HardDelete(int id)
    {
        await _service.HardDeleteAsync(id);
        return Ok();
    }
}