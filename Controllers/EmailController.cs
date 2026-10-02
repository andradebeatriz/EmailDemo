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
        try
        {
            await _emailService.EnviarAsync(email);
            return Ok("E-mail enviado!");
        }
        catch (Exception)
        {
            return StatusCode(502, "Não foi possível enviar o e-mail no momento.");
        }
    }
}