using DEBUG.BL.DTOs.AnswerDTOs;
using DEBUG.BL.Exceptions.Common.Common;
using DEBUG.BL.Services.AnswerServices;
using DEBUG.BL.Services.QuestionServices;
using DEBUG.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DEBUG.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AnswerController(IAnswerService _answerService, IQuestionService _questionService, UserManager<User> _userManager) : ControllerBase
{
    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllByQuestionId(int questionId)
    {
        if (await _questionService.GetByIdAsync(questionId) == null) throw new NotFoundException<Question>();
        return Ok(_answerService.GetAllByQuestionId(questionId));
    }
    //[HttpGet("[action]")]
    //public IActionResult GetAll()
    //{
    //    return Ok(_answerService.GetAll());
    //}
    [HttpGet("[action]")]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await _answerService.GetByIdAsync(id));
    }
    [HttpPost("[action]")]
    public async Task<IActionResult> Create(AnswerCreateDTO dto, int questionId)
    {
        var res = await _answerService.CreateAsync(questionId, dto, await _userManager.GetUserAsync(User));
        return Ok(res);
    }
    [HttpPut("[action]")]
    public async Task<IActionResult> Update(int id, AnswerUpdateDTO dto)
    {
        return Ok(await _answerService.UpdateAsync(id, dto));
    }
    [HttpPut("[action]")]
    public async Task<IActionResult> SoftDeleteOrRestore(int id)
    {
        await _answerService.SoftDeleteOrRestoreAsync(id);
        return Ok();
    }
    [HttpDelete("[action]")]
    public async Task<IActionResult> HardDelete(int id)
    {
        await _answerService.HardDeleteAsync(id);
        return Ok();
    }
}
