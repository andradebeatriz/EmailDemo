using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
public class EmailService : IEmailService
{
    private readonly EmailSettings _settings;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IOptions<EmailSettings> settings, ILogger<EmailService> logger)
    {
        _settings = settings.Value;
        _logger = logger;
    }

    public async Task EnviarAsync(EmailDto email)
    {
        var mensagem = new MimeMessage();
        mensagem.From.Add(new MailboxAddress(_settings.FromName, _settings.FromEmail));
        mensagem.To.Add(MailboxAddress.Parse(email.Para));
        mensagem.Subject = email.Assunto;
        mensagem.Body = new BodyBuilder { HtmlBody = email.Corpo }.ToMessageBody();

        using var smtp = new SmtpClient();
        try
        {
            await smtp.ConnectAsync(_settings.Host, _settings.Port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_settings.Username, _settings.Password);
            await smtp.SendAsync(mensagem);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Falha ao enviar e-mail para {Destino}", email.Para);
            throw;
        }
        finally
        {
            await smtp.DisconnectAsync(true);
        }
    }
}