using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SendGrid.Helpers.Mail;
using ChallengeAlkemyDisney.Models;
using ChallengeAlkemyDisney.Interfaces;

namespace ChallengeAlkemyDisney.Services
{
    public class MailService : IMailService
    {
        private readonly ISendGridClient _client;

        public MailService(ISendGridClient client)
        {
            _client = client;
        }

        public async Task SendMail(User user)
        {
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("test@example.com", "Api ChallengeDisney"),
                Subject = "Se ha registrado con éxito",
                PlainTextContent = $"Se ha creado el usuario con nombre {user.UserName} de manera correcta",
            };
            msg.AddTo(new EmailAddress(user.Email, "Test User"));
            await _client.SendEmailAsync(new SendGridMessage());
        }
    }
}
