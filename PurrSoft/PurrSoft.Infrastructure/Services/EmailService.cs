using PurrSoft.Application.Interfaces;
using PurrSoft.Application.Models;
using PurrSoft.Common.Config;
using System.Net;
using System.Net.Mail;

namespace PurrSoft.Infrastructure.Services;

public class EmailService(SmtpClientConfig smtpClientConfig) : IEmailService
{
    public async Task SendEmailAsync(EmailDto email)
    {
        using MailMessage mail = new()
        {
            From = new MailAddress(smtpClientConfig.Username),
            Subject = email.Subject,
            Body = email.Body,
            IsBodyHtml = true
        };

        // Add recipients
        mail.To.Add(email.To);

        if (email.CC != null)
        {
            foreach (string cc in email.CC)
            {
                mail.CC.Add(new MailAddress(cc));
            }
        }

        if (email.Bcc != null)
        {
            foreach (string bcc in email.Bcc)
            {
                mail.Bcc.Add(new MailAddress(bcc));
            }
        }

        using SmtpClient smtpClient = new(smtpClientConfig.Host, smtpClientConfig.Port)
        {
            EnableSsl = true,
            UseDefaultCredentials = false, // Ensure using explicit credentials
            Credentials = new NetworkCredential(
                smtpClientConfig.Username,
                smtpClientConfig.Password
            ),
            DeliveryMethod = SmtpDeliveryMethod.Network
        };

        try
        {
            await smtpClient.SendMailAsync(mail);
            Console.WriteLine("Email sent successfully.");
        }
        catch (SmtpException ex)
        {
            Console.WriteLine($"SMTP Exception: {ex.Message}");
            throw new InvalidOperationException("Failed to send email.", ex);
        }
    }
}