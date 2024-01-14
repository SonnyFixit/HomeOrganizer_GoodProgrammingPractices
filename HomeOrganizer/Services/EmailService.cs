using HomeOrganizer.Common;
using System.Net;
using System.Net.Mail;

namespace HomeOrganizer.Services
{
    /// <summary>
    /// Service for sending various types of emails related to the Home Organizer application.
    /// </summary>
    public static class EmailService
    {
        // HTML style to be applied to the email content.
        private static readonly string emailStyle = "<style>  " +
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
                "</style>";


        /// <summary>
        /// Sends an email with the specified details.
        /// </summary>
        /// <param name="toEmail">Recipient's email address.</param>
        /// <param name="toLogin">Recipient's login name.</param>
        /// <param name="subject">Subject of the email.</param>
        /// <param name="body">HTML body of the email.</param>
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


        // Various public methods to send specific types of emails (password reset, email change, contact us, etc.)
        // These methods construct the email's subject and body and use SendEmail for delivery.

        public static async Task SendResetPassword(string email, string login, string resetUrl)
        {
            string body = "<!DOCTYPE html>" +
                "<html lang=\"pl\">" +
                "<head>" +
                "<meta charset=\"UTF-8\">" +
                "<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">" +
                "<title>Password reset</title>" +
                emailStyle +
                "</head>" +
                    "<body>" +
                        "<div class=\"email-container\">" +
                        "<h2>Password reset</h2>" +
                        $"<p>Hello, {login}</p>" +
                        "<p>We have received a password reset request for your Home Organizer account.</p>" +
                        "<p>If this was you, click the link below to open password reset page <br/>" +
                        $"(you have {Security.passwordTokenExpirationTime.Minutes} minutes before link expires)</p>" +
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
                emailStyle +
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

        public static async Task SendEmailChangedCurrent(string email, string login)
        {
            string currentEmailBody = "<!DOCTYPE html>" +
               "<html lang=\"pl\">" +
               "<head>" +
               "<meta charset=\"UTF-8\">" +
               "<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">" +
               "<title>Password changed</title>" +
               emailStyle +
               "</head>" +
                   "<body>" +
                       "<div class=\"email-container\">" +
                       "<h2>Email changed!</h2>" +
                       $"<p>Hello, {login}</p>" +
                       "<p> Your email has been changed recently. </p>" +
                       "<p>If this was you, go check your new email adress for confirmation!</p>" +
                       "<p>Otherwise please contact us as fast as possible.</p> " +
                       "<p>Thank you!</p>" +
                       "<p>Home Organizer Team</p>" +
                       "</div>" +
                   "</body>" +
               "</html>";
            await SendEmail(email, login, "Email changed - old", currentEmailBody);
        }

        public static async Task SendEmailChangedNew(string newEmail, string login, string changeEmailUrl)
        {
            string body = "<!DOCTYPE html>" +
           "<html lang=\"pl\">" +
           "<head>" +
           "<meta charset=\"UTF-8\">" +
           "<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">" +
           "<title>Password changed</title>" +
           emailStyle +
           "</head>" +
               "<body>" +
                   "<div class=\"email-container\">" +
                   "<h2>Email changed!</h2>" +
                   $"<p>Hello, {login}</p>" +
                   "<p> Your email has been changed recently. </p>" +
                   $"<p>If this was you, click the link below to confirm email change: <br/>" +
                   $"(you have {Security.emailTokenExpirationTime.Minutes} minutes before link expires)</p>" +
                   $"<a class=\"button\" href=\"{changeEmailUrl}\">Change email</a>" +
                   "<p>If this wasn't you, please contact us as fast as possible.</p> " +
                   "<p>Thank you!</p>" +
                   "<p>Home Organizer Team</p>" +
                   "</div>" +
               "</body>" +
           "</html>";

            await SendEmail(newEmail, login, "Email changed - new", body);
        }

        public static async Task SendContactUs(string from, string issue, string content)
        {
            string body = "<!DOCTYPE html>" +
               "<html lang=\"pl\">" +
               "<head>" +
               "<meta charset=\"UTF-8\">" +
               "<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">" +
               "<title>USER CONTACT</title>" +
               emailStyle +
               "</head>" +
                   "<body>" +
                       "<div class=\"email-container\">" +
                       $"<h2>User contacted us!</h2>" +
                       $"<h4>User: {from}</h4>" +
                       $"<h4>Title: {issue}</h4>" +
                       "<h4>Content:</h4>" +
                       $"<p> {content} </p>" +
                       "</div>" +
                   "</body>" +
               "</html>";

            await SendEmail("home.organizer.333@gmail.com", "Home Organizer", "USER CONTACT", body);
        }

        public static async Task SendContactUsResponse(string email, string login, string issue)
        {
            string body = "<!DOCTYPE html>" +
               "<html lang=\"pl\">" +
               "<head>" +
               "<meta charset=\"UTF-8\">" +
               "<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">" +
               "<title>Password changed</title>" +
               emailStyle +
               "</head>" +
                   "<body>" +
                       "<div class=\"email-container\">" +
                       $"<h2>Thank you for contacting us!</h2>" +
                       $"<h4>Last time we received email with issue:</h4>" +
                       $"<h4>{issue}</h4>" +
                       "<p>We will look into it and check what can we do about it</p>" +
                       "<p>Home Organizer Team</p>" +
                       "</div>" +
                   "</body>" +
               "</html>";
            await SendEmail(email, login, "Thank you for contact!", body);
        }
    }
}
