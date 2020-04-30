namespace Golem.Core.Models.Settings
{
    public class SendGridSettings
    {
        public string ApiKey { get; set; }
        public string SendEmailsFrom { get; set; }
        public string[] SendEmailsTo { get; set; }
    }
}
