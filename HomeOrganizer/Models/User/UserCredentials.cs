using HomeOrganizer.Common;

namespace HomeOrganizer.Models.User
{
    public class UserCredentials
    {
        public string Login { get; init; }

        public string PasswordHash { get; private set; }
        public string PasswordSalt { get; private set; }


        public UserCredentials(string login, string password)
        {
            Login = login;
            PasswordSalt = Security.GenerateSalt();
            PasswordHash = Security.HashPassword(password, PasswordSalt);
        }

        public void ResetPassword(string newPassword)
        {
            PasswordSalt = Security.GenerateSalt();
            PasswordHash = Security.HashPassword(newPassword, PasswordSalt);
        }

        public bool CheckPassword(string provided)
        {
            if (PasswordHash == null || PasswordSalt == null) return false;
            return Security.CheckPassword(provided, PasswordHash, PasswordSalt);
        }
    }
}
