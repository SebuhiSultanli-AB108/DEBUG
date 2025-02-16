using DEBUG.BL.DTOs.ReportItemDTOs;
using DEBUG.BL.Services.ReportServices;
using Microsoft.AspNetCore.Mvc;

namespace DEBUG.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReportController(IReportService _service) : ControllerBase
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
        var res = await _service.CreateAsync(dto);
        return Ok(res);
    }
    [HttpDelete("[action]")]
    public async Task<IActionResult> HardDelete(int id)
    {
        await _service.HardDeleteAsync(id);
        return Ok();
    }
}