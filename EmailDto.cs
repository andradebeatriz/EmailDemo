using System.ComponentModel.DataAnnotations;

public record EmailDto(
    [Required, EmailAddress] string Para,
    [Required, StringLength(150)] string Assunto,
    [Required] string Corpo);