using DEBUG.BL.DTOs.CategoryDTOs;
using DEBUG.BL.Services.CategoryServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DEBUG.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CategoryController(ICategoryService _service) : ControllerBase
{
    [HttpGet("[action]")]
    public async Task<IActionResult> GetAll(short pageNo, short take)
    {
        return Ok(await _service.GetAllAsync(pageNo, take));
    }
    [HttpGet("[action]")]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await _service.GetByIdAsync(id));
    }
    [Authorize(Roles = "Moderator,Admin")]
    [HttpPost("[action]")]
    public async Task<IActionResult> Create(CategoryCreateDTO dto)
    {
        var res = await _service.CreateAsync(dto);
        return Ok(res);
    }
    [Authorize(Roles = "Moderator,Admin")]
    [HttpPut("[action]")]
    public async Task<IActionResult> Update(int id, CategoryUpdateDTO dto)
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
