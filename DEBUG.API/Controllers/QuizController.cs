using DEBUG.BL.DTOs.QuizQuestionDTOs;
using DEBUG.BL.Services.QuizQuestionServices;
using Microsoft.AspNetCore.Mvc;

namespace DEBUG.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController(IQuizQuestionService _service) : ControllerBase
    {
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _service.GetAllAsync());
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetLevelAsync(int difficulty)
        {
            return Ok(await _service.Get5RandomQuestionsAsync(difficulty));
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Create(QuizQuestionCreateDTO dto)
        {
            var res = await _service.CreateAsync(dto);
            return Ok(res);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> RangedCreate(IEnumerable<QuizQuestionCreateDTO> dtos)
        {
            await _service.RangedCreateAsync(dtos);
            return Ok();
        }
        [HttpPut("[action]")]
        public async Task<IActionResult> Update(int id, QuizQuestionUpdateDTO dto)
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
}
