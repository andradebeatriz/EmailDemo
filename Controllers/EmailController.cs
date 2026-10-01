using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class EmailController : ControllerBase
{
    private readonly IEmailService _emailService;
    public EmailController(IEmailService emailService) => _emailService = emailService;

    [HttpPost]
    public async Task<IActionResult> Enviar([FromBody] EmailDto email)
    {
        await _emailService.EnviarAsync(email);
        return Ok("E-mail enviado!");
    }
}