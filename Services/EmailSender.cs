using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using HtHInAction.Models;
using Microsoft.Extensions.Options;


public interface IEmailSender
{
    Task SendEmailAsync(Mail mailTemplate,  List<string> toEmail);
}

public class EmailSender : IEmailSender
    {
        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public EmailSettings _emailSettings { get; }

        public Task SendEmailAsync(Mail mailTemplate, List<string> toEmail)
        {
            Execute (mailTemplate,toEmail).Wait();
            return Task.FromResult(0);
        }

        public async Task Execute(Mail mailTemplate, List<string> toEmail)
        {
           foreach(var mail in toEmail)
                await Send(mailTemplate, mail);
        }

        private async Task Send(Mail mailTemplate, string toEmail)
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

            using (SmtpClient smtp = new SmtpClient(_emailSettings.PrimaryDomain, _emailSettings.PrimaryPort))
            {
                smtp.Credentials = new NetworkCredential(_emailSettings.UsernameEmail, _emailSettings.UsernamePassword);
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(mail);
            }
          }
          catch(Exception exc)
          {
              var message = exc.ToString();
          }
        }
    }