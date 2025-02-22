using DEBUG.BL.DTOs.AnswerDTOs;
using DEBUG.BL.Exceptions.Common.Common;
using DEBUG.BL.Extensions;
using DEBUG.BL.Services.AnswerServices;
using DEBUG.BL.Services.QuestionServices;
using DEBUG.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DEBUG.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AnswerController(
    IAnswerService _answerService,
    IQuestionService _questionService,
    UserManager<User> _userManager,
    IWebHostEnvironment _wwwRoot) : ControllerBase
{
    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllByQuestionId(int questionId)
    {
        if (await _questionService.GetByIdAsync(questionId) == null) throw new NotFoundException<Question>();
        return Ok(await _answerService.GetAllByQuestionIdAsync(questionId));
    }
    [HttpGet("[action]")]
    public async Task<IActionResult> GetByUserIdAsync(string id)
    {
        return Ok(await _answerService.GetByUserIdAsync(id));
    }
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
    [HttpPost("[action]")]
    public async Task<IActionResult> LikeDislike(int answerId, bool isLiked)
    {
        User? user = await _userManager.GetUserAsync(User);
        if (user == null) throw new NotFoundException<User>();
        await _answerService.LikeDislikeAsync(user, answerId, isLiked);
        return Ok();
    }
    [HttpGet("[action]")]
    public async Task<IActionResult> GetLikesAndDislikes(int answerId)
    {
        return Ok(await _answerService.GetLikeDislikeAsync(answerId));
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
    [HttpPost("[action]")]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        return Ok(await file.UploadAsync(_wwwRoot, "answer"));
    }
}