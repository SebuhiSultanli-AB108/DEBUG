using DEBUG.BL.DTOs.QuizQuestionDTOs;
using DEBUG.BL.Exceptions.Common.Common;
using DEBUG.BL.Services.QuizQuestionServices;
using DEBUG.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DEBUG.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController(IQuizQuestionService _service, UserManager<User> _userManager) : ControllerBase
    {
        [Authorize(Roles = "Moderator,Admin")]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllAsync(short pageNo, short take)
        {
            return Ok(await _service.GetAllAsync(pageNo, take));
        }
        [Authorize(Roles = "User")]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetLevelAsync(byte difficulty, int tagId, short take)
        {
            return Ok(await _service.GetRandomQuestionsAsync(difficulty, tagId, take));
        }
        [Authorize(Roles = "User")]
        [HttpPost("[action]")]
        public async Task<IActionResult> VerifyQuizAnswers(int questionId, int answerId)
        {
            User? user = await _userManager.GetUserAsync(User);
            if (user == null) throw new NotFoundException<User>();
            return Ok(await _service.VerifyQuizAnswersAsync(questionId, answerId, user));
        }
        [Authorize(Roles = "Moderator,Admin")]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }
        [Authorize(Roles = "Moderator,Admin")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Create(QuizQuestionCreateDTO dto)
        {
            var res = await _service.CreateAsync(dto);
            return Ok(res);
        }
        [Authorize(Roles = "Moderator,Admin")]
        [HttpPost("[action]")]
        public async Task<IActionResult> RangedCreate(IEnumerable<QuizQuestionCreateDTO> dtos)
        {
            await _service.RangedCreateAsync(dtos);
            return Ok();
        }
        [Authorize(Roles = "Moderator,Admin")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Update(int id, QuizQuestionUpdateDTO dto)
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
}
