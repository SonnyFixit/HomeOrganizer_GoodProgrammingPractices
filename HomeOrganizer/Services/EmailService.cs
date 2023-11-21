using System.Net;
using System.Net.Mail;

namespace HomeOrganizer.Services
{
    public static class EmailService
    {
        private static async Task SendEmail(string toEmail, string toLogin, string subject, string body)
        {
            using var client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("home.organizer.333@gmail.com", "zlcgbvthwvdbxfuu");
            using var message = new MailMessage(
                from: new MailAddress("home.organizer.333@gmail.com", "Home Organizer"),
                to: new MailAddress(toEmail, toLogin)
                );

            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            await client.SendMailAsync(message);
        }

        public static async Task SendResetPassword(string email, string login, string resetUrl)
        {
            string body = "<!DOCTYPE html>" +
                "<html lang=\"pl\">" +
                "<head>" +
                "<meta charset=\"UTF-8\">" +
                "<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">" +
                "<title>Resetowanie hasła</title>" +
                "<style>  " +
                "body {" +
                "font-family: Arial, sans-serif;" +
                "background-color: #f2f2f2;" +
                "}" +
                ".email-container {" +
                "max-width: 600px;" +
                "margin: 0 auto;" +
                "padding: 20px;" +
                "background-color: #ffffff;" +
                "border: 1px solid #dddddd;    }" +
                "h2 {" +
                "color: #333333;" +
                "}" +
                "p {" +
                "color: #666666;" +
                "margin-bottom: 20px;" +
                "}" +
                ".button {" +
                "display: inline-block;" +
                "padding: 10px 20px;" +
                "background-color: #FFEAEA;" +
                "color: #000000;" +
                "text-decoration: none;" +
                "border-radius: 3px;" +
                "}" +
                "a, a:link, a:visited, a:hover, a:active {" +
                "color: #000000;" +
                "}" +
                "</style>" +
                "</head>" +
                    "<body>" +
                        "<div class=\"email-container\">" +
                        "<h2>Resetowanie hasła</h2>" +
                        $"<p>Witaj, {login}</p>" +
                        "<p>Otrzymaliśmy prośbę o zresetowanie hasła dla Twojego konta Home Organizer.</p>" +
                        "<p>Jeśli to byłeś Ty, kliknij poniższy link, aby przejść do strony resetowania hasła:</p>" +
                        $"<a class=\"button\" href=\"{resetUrl}\">Zresetuj hasło</a>" +
                        "<p>Jeśli to nie byłeś Ty, zignoruj tę wiadomość.</p> " +
                        "<p>Dziękujemy!</p>" +
                        "<p>Zespół Home Organizer</p>" +
                        "</div>" +
                    "</body>" +
                "</html>";

            await SendEmail(email, login, "Password reset", body);
        }
    }
}
