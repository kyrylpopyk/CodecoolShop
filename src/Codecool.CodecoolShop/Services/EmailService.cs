using System.Linq;
using Codecool.CodecoolShop.Core.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;


namespace Codecool.CodecoolShop.Services
{
    public interface IEmailService
    {
        void Send(string from, string to, string subject, string html);
        string GetHtml(Order order);
    }

    public class EmailService : IEmailService
    {
        private readonly IConfigurationSection _smtpData;

        public EmailService(IConfiguration configuration)
        {
            _smtpData = configuration.GetSection("Smtp");
        }
        public void Send(string from, string to, string subject, string html)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            using var smtp = new SmtpClient();
            smtp.Connect(_smtpData["Address"], int.Parse(_smtpData["Port"]), SecureSocketOptions.StartTls);
            smtp.Authenticate(_smtpData["User"], _smtpData["Password"]);
            smtp.Send(email);
            smtp.Disconnect(true);
        }

        public string GetHtml(Order order)
        {
            var result = $"<h2>Hello, {order.User.FirstName}!</h2> <p>Your order details:</p><ul>";
            order.Items.ForEach(i => result += ($"<li><p>{i.Product.Name} x {i.Quantity} = {i.Price * i.Quantity}</p></li>"));
            result += $"</ul><p><b>Total: {order.Items.Sum(i => i.Price * i.Quantity)}</b></p>";

            return result;
        }
    }
}
