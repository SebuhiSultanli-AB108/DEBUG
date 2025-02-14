using DEBUG.BL.DTOs.QuestionDTOs;
using DEBUG.BL.Extensions;
using DEBUG.BL.Services.QuestionServices;
using DEBUG.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DEBUG.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class QuestionController(IQuestionService _service, UserManager<User> _userManager, IWebHostEnvironment _wwwRoot) : ControllerBase
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
    public async Task<IActionResult> Create(int categoryId, QuestionCreateDTO dto)
    {
        var res = await _service.CreateAsync(categoryId, dto, await _userManager.GetUserAsync(User));
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
    [HttpPost("[action]")]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        return Ok(await file.UploadAsync(_wwwRoot, "question"));
    }
}