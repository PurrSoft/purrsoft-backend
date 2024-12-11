using PurrSoft.Application.Models;

namespace PurrSoft.Application.Interfaces;

public interface IEmailService
{
    Task SendEmailAsync(EmailDto email);
}
