using MimeKit;
using MailKit.Net.Smtp;
namespace WebApplication1.Services
{
    public class EmailService: IEmailService
    {
        public void SendEmail(string to, string subject, string body)
        {
            var environmentEmail = Environment.GetEnvironmentVariable("email");
            var environmentUsername = Environment.GetEnvironmentVariable("username");
            var environmentPassword = Environment.GetEnvironmentVariable("password");
            var environmentSMTP = Environment.GetEnvironmentVariable("smtp");


            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Itamar Tati", environmentEmail));
            message.To.Add(new MailboxAddress("", to));
            message.Subject = subject;

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = body;

            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.Connect(environmentSMTP, 587, false);
                client.Authenticate(environmentUsername, environmentPassword);
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
