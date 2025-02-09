using DEBUG.BL.DTOs.QuestionDTOs;
using DEBUG.BL.Services.QuestionServices;
using DEBUG.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DEBUG.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QuestionController(IQuestionService _service, UserManager<User> _userManager) : ControllerBase
{
    [HttpGet("[action]")]
    public IActionResult GetAll()
    {
        return Ok(_service.GetAll());
    }
    [HttpGet("[action]")]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await _service.GetByIdAsync(id));
    }
    [HttpPost("[action]")]
    public async Task<IActionResult> Create(QuestionCreateDTO dto)
    {
        var res = await _service.CreateAsync(dto, await _userManager.GetUserAsync(User));
        return Ok(res);
    }
    [HttpPut("[action]")]
    public async Task<IActionResult> Update(int id, QuestionUpdateDTO dto)
    {
        return Ok(await _service.UpdateAsync(id, dto));
    }
    [HttpPut("[action]")]
    public async Task<IActionResult> SoftDeleteOrRestore(int id)
    {
        await _service.SoftDeleteOrRestoreAsync(id);
        return Ok();
    }
    [HttpDelete("[action]")]
    public async Task<IActionResult> HardDelete(int id)
    {
        await _service.HardDeleteAsync(id);
        return Ok();
    }
}
