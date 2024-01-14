using HomeOrganizer.Common;

namespace HomeOrganizer.Models.User
{

    /// <summary>
    /// Manages the credentials of a user, including login and hashed password.
    /// </summary>
    public class UserCredentials
    {
        // User's login identifier.
        public string Login { get; init; }

        // Hashed password and salt for security.
        public string PasswordHash { get; private set; }
        public string PasswordSalt { get; private set; }


        /// <summary>
        /// Constructor that initializes a new UserCredentials instance with a login and password.
        /// The password is securely hashed during initialization.
        /// </summary>
        /// <param name="login">The user's login name.</param>
        /// <param name="password">The user's password.</param>
        public UserCredentials(string login, string password)
        {
            Login = login;
            PasswordSalt = Security.GenerateSalt();
            PasswordHash = Security.HashPassword(password, PasswordSalt);
        }

        /// <summary>
        /// Resets the user's password by generating a new salt and hashing the new password.
        /// </summary>
        /// <param name="newPassword">The new password for the user.</param>
        public void ResetPassword(string newPassword)
        {
            PasswordSalt = Security.GenerateSalt();
            PasswordHash = Security.HashPassword(newPassword, PasswordSalt);
        }

        /// <summary>
        /// Checks if a provided password matches the stored hashed password.
        /// </summary>
        /// <param name="provided">The provided password to check.</param>
        /// <returns>True if the password is correct, otherwise false.</returns>
        public bool CheckPassword(string provided)
        {
            if (PasswordHash == null || PasswordSalt == null) return false;
            return Security.CheckPassword(provided, PasswordHash, PasswordSalt);
        }
    }
}
