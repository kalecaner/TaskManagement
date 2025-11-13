using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Interfaces;

namespace TaskManagement.Persistance.Services.Email
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly  SmtpSettings _smtpSettings;

        public SmtpEmailSender(IOptions<SmtpSettings> options)
        {
            _smtpSettings = options.Value;
        }

        public async Task SendAsync(string to, string subject, string htmlBody)
        {
             using var  client= new  SmtpClient(_smtpSettings.Host.Trim(), _smtpSettings.Port);
            {
                client.EnableSsl = _smtpSettings.EnableSsl;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Credentials = new NetworkCredential(_smtpSettings.UserName.Trim(), _smtpSettings.Password.Trim());


            };
            using var mail= new MailMessage();
            {
                mail.From = new MailAddress(_smtpSettings.From.Trim());
                mail.To.Add(to);
                mail.Subject = subject;
                mail.Body = htmlBody;
                mail.IsBodyHtml = true;
            };

            await client.SendMailAsync(mail);

        }
    }
}
