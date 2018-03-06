using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using HtHInAction.Models;
using HtHInAction.Repositories;
using Microsoft.Extensions.Options;
using OpenPop.Mime;
using OpenPop.Pop3;

public interface IEmailService
{
    Task SendEmailAsync(Mail mailTemplate, List<string> toEmail);
    Task<int> ReceivedEmailAsync();
}

public class EmailService : IEmailService
{
    private IRepository<EmailSettings> _emailSettingsRepository;
    private IRepository<MailSentAndReceived> _mailSentAndReceivedRepository;

    public EmailService(IRepository<EmailSettings> emailSettingsRepository, IRepository<MailSentAndReceived> mailSentAndReceivedRepository)
    {
        _emailSettingsRepository = emailSettingsRepository;
        _mailSentAndReceivedRepository = mailSentAndReceivedRepository;
    }

    public async Task SendEmailAsync(Mail mailTemplate, List<string> toEmail)
    {
        var emails = await _emailSettingsRepository.Find(x=>true);
        var defaultEmail = emails.FirstOrDefault();

        await Execute(mailTemplate, toEmail, defaultEmail);
    }

    public async Task<int> ReceivedEmailAsync()
    {
        var pop3Client = new Pop3Client();

        try
        {
            var emails = await _emailSettingsRepository.Find(x => true);
            var defaultEmail = emails.FirstOrDefault();


            if (pop3Client.Connected) pop3Client.Disconnect();

            pop3Client.Connect(defaultEmail.DomainPop, defaultEmail.PortPop, true);
            pop3Client.Authenticate(defaultEmail.Email, defaultEmail.Password);
            int count = pop3Client.GetMessageCount();

            for (int i = 1; i < count+1; i++)
            {
                try
                {
                    var message = pop3Client.GetMessage(i);
                    var messageId = message.Headers.MessageId.ToString();
                    
                    var mailFind = await _mailSentAndReceivedRepository.FindOne(x=> x.MessageId == messageId);
                    if(mailFind!=null) continue;

                    var mail = new MailSentAndReceived();
                    mail.To = message.Headers.To.FirstOrDefault().Address;
                    mail.From = message.Headers.From.MailAddress.Address;                    
                    mail.Subject = message.Headers.Subject;
                    mail.MessageId = message.Headers.MessageId.ToString();
                    mail.Date = DateTime.Now;
                    mail.Type = "Received";

                    var messagePart = message.MessagePart.MessageParts[1];
                    mail.Body = messagePart.BodyEncoding.GetString(messagePart.Body);

                    await _mailSentAndReceivedRepository.Add(mail);                    
                }
                catch (Exception exc)
                {
                    var message = exc.ToString();
                }
            }

            return count;
        }
        catch (Exception exc)
        {
            var message = exc.ToString();
            return 0;
        }
        finally
        {
            if (pop3Client.Connected) pop3Client.Disconnect();
        }
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

            using (SmtpClient smtp = new SmtpClient(defaultEmail.DomainSmtp, defaultEmail.PortSmtp))
            {
                smtp.Credentials = new NetworkCredential(defaultEmail.Email, defaultEmail.Password);
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(mail);
            }

            await _mailSentAndReceivedRepository.Add(
                new MailSentAndReceived
                {
                    From = mailTemplate.From,
                    To = toEmail,
                    Subject = mailTemplate.Subject,
                    Body = mailTemplate.Body,
                    Date = DateTime.Now,
                    Type = "Sent"
                });
        }
        catch (Exception exc)
        {
            var message = exc.ToString();
        }
    }
}