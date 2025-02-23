using Microsoft.AspNetCore.Mvc;

namespace DEBUG.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MeetController : ControllerBase
{
    [HttpGet("[action]")]
    public IActionResult CreateRoom()
    {
        string Url = $"https://meet.jit.si/{Guid.NewGuid()}";
        return Ok(Url);
    }
}
