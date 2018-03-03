using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using HtHInAction.Models;
using HtHInAction.Repositories;
using Microsoft.Extensions.Options;


public interface IEmailSender
{
    Task SendEmailAsync(Mail mailTemplate, List<string> toEmail);
}

public class EmailSender : IEmailSender
{
    private IRepository<EmailSettings> _emailSettingsRepository;

    public EmailSender(IRepository<EmailSettings> emailSettingsRepository)
    {
        _emailSettingsRepository = emailSettingsRepository;
    }    

    public async Task SendEmailAsync(Mail mailTemplate, List<string> toEmail)
    {
        var emails = await _emailSettingsRepository.Find(x=>x.Default);
        var defaultEmail = emails.FirstOrDefault();

        await Execute(mailTemplate, toEmail, defaultEmail);
    }

    public async Task Execute(Mail mailTemplate, List<string> toEmail, EmailSettings defaultEmail)
    {
        foreach (var mail in toEmail)
            await Send(mailTemplate, mail, defaultEmail);
    }

    private async Task Send(Mail mailTemplate, string toEmail, EmailSettings defaultEmail)
    {
        try
        {
            var mail = new MailMessage()
            {
                From = new MailAddress(mailTemplate.From, mailTemplate.From)
            };

            mail.To.Add(new MailAddress(toEmail));

            mail.Subject = mailTemplate.Subject;
            mail.Body = mailTemplate.Body;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;

            using (SmtpClient smtp = new SmtpClient(defaultEmail.Domain, defaultEmail.Port))
            {
                smtp.Credentials = new NetworkCredential(defaultEmail.Email, defaultEmail.Password);
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(mail);
            }
        }
        catch (Exception exc)
        {
            var message = exc.ToString();
        }
    }
}