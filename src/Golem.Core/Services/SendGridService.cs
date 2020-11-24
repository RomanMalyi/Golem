using System.Net;
using System.Threading.Tasks;
using Golem.Core.Models;
using Golem.Core.Models.Dto.Requests;
using Golem.Core.Models.Settings;
using Golem.Core.Services.Abstractions;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Golem.Core.Services
{
    public class SendGridService : IEmailService
    {
        private readonly SendGridSettings sendGridSettings;

        public SendGridService(SendGridSettings sendGridSettings)
        {
            this.sendGridSettings = sendGridSettings;
        }

        public async Task<bool> SendEmail(EmailModel model)
        {
            //TODO:add logging
            var client = new SendGridClient(sendGridSettings.ApiKey);
            var from = new EmailAddress(sendGridSettings.SendEmailsFrom, "Golem");
            var message = $"From: {model.SenderFullName}, Email: {model.SenderEmail} Message: {model.Message}";

            Response response = null;
            foreach (var email in sendGridSettings.SendEmailsTo)
            {
                var to = new EmailAddress(email);

                var msg = MailHelper.CreateSingleEmail(from, to, "Golem contact us", message, null);
                response = await client.SendEmailAsync(msg);
            }


            return response != null && response.StatusCode == HttpStatusCode.Accepted;
        }
    }
}
