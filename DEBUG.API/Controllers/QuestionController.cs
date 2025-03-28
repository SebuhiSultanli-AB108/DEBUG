﻿using DEBUG.BL.DTOs.QuestionDTOs;
using DEBUG.BL.Exceptions.Common.Common;
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
    public async Task<IActionResult> GetAllAsync(short pageNo, short take)
    {
        return Ok(await _service.GetAllAsync(pageNo, take));
    }
    [HttpGet("[action]")]
    public async Task<IActionResult> GetByUserIdAsync(string id, short pageNo, short take)
    {
        return Ok(await _service.GetByUserIdAsync(pageNo, take, id));
    }
    [HttpGet("[action]")]
    public async Task<IActionResult> GetByCategoryAndTagsAsync(int categoryId, [FromQuery] int[] tagIds, short pageNo, short take)
    {
        return Ok(await _service.GetByCategoryAndTagsAsync(pageNo, take, categoryId, tagIds));
    }
    [HttpGet("[action]")]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await _service.GetByIdAsync(id));
    }
    [Authorize(Roles = "User")]
    [HttpPost("[action]")]
    public async Task<IActionResult> Create(int categoryId, QuestionCreateDTO dto)
    {
        User? user = await _userManager.GetUserAsync(User);
        if (user == null) throw new NotFoundException<User>();
        var res = await _service.CreateAsync(categoryId, dto, user);
        return Ok(res);
    }
    [Authorize(Roles = "User")]
    [HttpPost("[action]")]
    public async Task<IActionResult> LikeDislike(int questionId, bool isLiked)
    {
        User? user = await _userManager.GetUserAsync(User);
        if (user == null) throw new NotFoundException<User>();
        await _service.LikeDislikeAsync(user, questionId, isLiked);
        return Ok();
    }
    [HttpGet("[action]")]
    public async Task<IActionResult> GetLikesAndDislikes(int questionId)
    {
        return Ok(await _service.GetLikeDislikeAsync(questionId));
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
    [Authorize(Roles = "Moderator,Admin")]
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