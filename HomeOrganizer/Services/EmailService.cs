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
                "<title>Password reset</title>" +
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
                        "<h2>Password reset</h2>" +
                        $"<p>Hello, {login}</p>" +
                        "<p>We have received a password reset request for your Home Organizer account.</p>" +
                        "<p>If this was you, click the link below to go to the password reset page:</p>" +
                        $"<a class=\"button\" href=\"{resetUrl}\">Reset password</a>" +
                        "<p>If this wasn't you, please contact us as fast as possible.</p> " +
                        "<p>Thank you!</p>" +
                        "<p>Home Organizer Team</p>" +
                        "</div>" +
                    "</body>" +
                "</html>";

            await SendEmail(email, login, "Password reset", body);
        }

        public static async Task SendChangedPasswordNotification(string email, string login)
        {
            string body = "<!DOCTYPE html>" +
                "<html lang=\"pl\">" +
                "<head>" +
                "<meta charset=\"UTF-8\">" +
                "<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">" +
                "<title>Password changed</title>" +
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
                        "<h2>Password changed!</h2>" +
                        $"<p>Hello, {login}</p>" +
                        "<p> Your password has been changed recently. </p>" +
                        "<p>If this was you, you can ignore this message.</p>" +
                        "<p>Otherwise please contact us as fast as possible.</p> " +
                        "<p>Thank you!</p>" +
                        "<p>Home Organizer Team</p>" +
                        "</div>" +
                    "</body>" +
                "</html>";

            await SendEmail(email, login, "Password changed", body);
        }
    }
}
