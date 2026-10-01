public interface IEmailService
{
    Task EnviarAsync(EmailDto email);
}