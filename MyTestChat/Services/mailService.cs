using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTestChat.Services
{
    public class mailService
    {
        public async Task SendEmailAsyns(string email, string subject, string message)
        {
            var emailmessage = new MimeMessage();

            emailmessage.From.Add(new MailboxAddress("Администрация сайта", "MyTestChat2020"));
            emailmessage.To.Add(new MailboxAddress("", email));
            emailmessage.Subject = subject;
            emailmessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 465, true);
                await client.AuthenticateAsync("mytestchat2020@gmail.com", "mysalaryis15000");
                await client.SendAsync(emailmessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}