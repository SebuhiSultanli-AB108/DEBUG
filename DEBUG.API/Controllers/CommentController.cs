using DEBUG.BL.DTOs.CommentDTOs;
using DEBUG.BL.Exceptions.Common.Common;
using DEBUG.BL.Services.AnswerServices;
using DEBUG.BL.Services.CommentServices;
using DEBUG.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DEBUG.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CommentController(IAnswerService _answerService, ICommentService _commentService, UserManager<User> _userManager) : ControllerBase
{
    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllByAnswerId(int answerId)
    {
        if (await _answerService.GetByIdAsync(answerId) == null) throw new NotFoundException<Answer>();
        return Ok(await _commentService.GetAllByAnswerIdAsync(answerId));
    }
    [HttpGet("[action]")]
    public async Task<IActionResult> GetByUserIdAsync(string id)
    {
        return Ok(await _commentService.GetByUserIdAsync(id));
    }
    [HttpGet("[action]")]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await _commentService.GetByIdAsync(id));
    }
    [HttpPost("[action]")]
    public async Task<IActionResult> Create(CommentCreateDTO dto, int answerId)
    {
        var res = await _commentService.CreateAsync(answerId, dto, await _userManager.GetUserAsync(User));
        return Ok(res);
    }
    [HttpPost("[action]")]
    public async Task<IActionResult> LikeDislike(int commentId, bool isLiked)
    {
        User? user = await _userManager.GetUserAsync(User);
        if (user == null) throw new NotFoundException<User>();
        await _commentService.LikeDislikeAsync(user, commentId, isLiked);
        return Ok();
    }
    [HttpGet("[action]")]
    public async Task<IActionResult> GetLikesAndDislikes(int commentId)
    {
        return Ok(await _commentService.GetLikeDislikeAsync(commentId));
    }
    [HttpPut("[action]")]
    public async Task<IActionResult> Update(int id, CommentUpdateDTO dto)
    {
        return Ok(await _commentService.UpdateAsync(id, dto));
    }
    [HttpPut("[action]")]
    public async Task<IActionResult> SoftDeleteOrRestore(int id)
    {
        await _commentService.SoftDeleteOrRestoreAsync(id);
        return Ok();
    }
    [HttpDelete("[action]")]
    public async Task<IActionResult> HardDelete(int id)
    {
        await _commentService.HardDeleteAsync(id);
        return Ok();
    }
}