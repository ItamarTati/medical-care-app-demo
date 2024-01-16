using MimeKit;
using MailKit.Net.Smtp;
namespace WebApplication1.Services
{
    public class EmailService
    {
        public void SendEmail(string to, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Your Name", "your-email@example.com"));
            message.To.Add(new MailboxAddress("", to));
            message.Subject = subject;

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = body;

            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.example.com", 587, false);
                client.Authenticate("your-username", "your-password");
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
