using DEBUG.BL.DTOs.TagDTOs;
using DEBUG.BL.Services.TagServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DEBUG.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TagController(ITagService _service) : ControllerBase
{
    [HttpGet("[action]")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }
    [HttpGet("[action]")]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await _service.GetByIdAsync(id));
    }
    [Authorize(Roles = "Moderator,Admin")]
    [HttpPost("[action]")]
    public async Task<IActionResult> Create(TagCreateDTO dto)
    {
        var res = await _service.CreateAsync(dto);
        return Ok(res);
    }
    [Authorize(Roles = "Moderator,Admin")]
    [HttpPut("[action]")]
    public async Task<IActionResult> Update(int id, TagUpdateDTO dto)
    {
        return Ok(await _service.UpdateAsync(id, dto));
    }
    [Authorize(Roles = "Moderator,Admin")]
    [HttpPut("[action]")]
    public async Task<IActionResult> SoftDeleteOrRestore(int id)
    {
        await _service.SoftDeleteOrRestoreAsync(id);
        return Ok();
    }
    [Authorize(Roles = "Moderator,Admin")]
    [HttpDelete("[action]")]
    public async Task<IActionResult> HardDelete(int id)
    {
        await _service.HardDeleteAsync(id);
        return Ok();
    }
}
