using System.Security.Cryptography;
using System.Text;

using HomeOrganizer.Models.User;

namespace HomeOrganizer.Common
{
    public static class Security
    {
        // Security parameters for password hashing
        private static readonly int keySize = 64;
        private static readonly int iterations = 350000;
        private static readonly HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

        // Security parameters for reset token
        private static readonly int resetTokenSize = 32;
        private static readonly int resetTokenIterations = 500000;

        // Expiration times for tokens
        public static readonly TimeSpan passwordTokenExpirationTime = new TimeSpan(0, 10, 0);
        public static readonly TimeSpan emailTokenExpirationTime = new TimeSpan(0, 30, 0);

        /// <summary>
        /// Generates a salt string using a cryptographically secure random number generator.
        /// </summary>
        /// <returns>A hexadecimal string representing the salt.</returns>
        public static string GenerateSalt()
        {
            byte[] salt = RandomNumberGenerator.GetBytes(keySize);
            return Convert.ToHexString(salt);
        }

        /// <summary>
        /// Hashes a password using PBKDF2 with the provided salt.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <param name="salt">The salt to use in hashing.</param>
        /// <returns>A hexadecimal string representing the hashed password.</returns>
        public static string HashPassword(string password, string salt)
        {
            var hashedPassword = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                Convert.FromHexString(salt),
                iterations,
                hashAlgorithm,
                keySize);

            return Convert.ToHexString(hashedPassword);
        }

        /// <summary>
        /// Checks if the provided password matches the hashed password.
        /// </summary>
        /// <param name="providedPassword">The password provided by the user.</param>
        /// <param name="hashedPassword">The stored hashed password.</param>
        /// <param name="salt">The salt used in the original hashing.</param>
        /// <returns>True if the passwords match, false otherwise.</returns>
        public static bool CheckPassword(string providedPassword, string hashedPassword, string salt)
        {
            var hashedProvided = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(providedPassword),
                Convert.FromHexString(salt),
                iterations,
                hashAlgorithm,
                keySize);

            return CryptographicOperations.FixedTimeEquals(hashedProvided, Convert.FromHexString(hashedPassword));
        }

        /// <summary>
        /// Creates reset password data for a given user login.
        /// </summary>
        /// <param name="login">The login of the user.</param>
        /// <returns>A ResetPasswordData object containing the token and other relevant information.</returns>
        public static ResetPasswordData CreateResetPasswordData(string login)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(keySize);
            byte[] token = RandomNumberGenerator.GetBytes(resetTokenSize);

            var hashedToken = Rfc2898DeriveBytes.Pbkdf2(
                token,
                salt,
                resetTokenIterations,
                hashAlgorithm,
                resetTokenSize);

            return new ResetPasswordData()
            {
                UserLogin = login,
                Token = Convert.ToHexString(token),
                HashedToken = Convert.ToHexString(hashedToken),
                Salt = Convert.ToHexString(salt),
                CreationTime = DateTime.UtcNow,
            };
        }

        /// <summary>
        /// Creates change email data for a given user login.
        /// </summary>
        /// <param name="login">The login of the user.</param>
        /// <param name="newEmail">The new email address to be associated with the user.</param>
        /// <returns>A ChangeEmailData object containing the token and other relevant information.</returns>
        public static ChangeEmailData CreateChangeEmailData(string login, string newEmail)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(keySize);
            byte[] token = RandomNumberGenerator.GetBytes(resetTokenSize);

            var hashedToken = Rfc2898DeriveBytes.Pbkdf2(
                token,
                salt,
                resetTokenIterations,
                hashAlgorithm,
                resetTokenSize);

            return new ChangeEmailData()
            {
                UserLogin = login,
                Token = Convert.ToHexString(token),
                HashedToken = Convert.ToHexString(hashedToken),
                Salt = Convert.ToHexString(salt),
                CreationTime = DateTime.UtcNow,
                NewEmail = newEmail
            };
        }

        /// <summary>
        /// Checks if a password reset link has expired.
        /// </summary>
        /// <param name="creationTime">The creation time of the reset link.</param>
        /// <returns>True if the link has expired, false otherwise.</returns>
        public static bool IsPasswordLinkExpired(DateTime creationTime)
        {
            DateTime expirationTime = creationTime.Add(passwordTokenExpirationTime);
            return expirationTime < DateTime.UtcNow;
        }

        /// <summary>
        /// Checks if an email change link has expired.
        /// </summary>
        /// <param name="creationTime">The creation time of the email change link.</param>
        /// <returns>True if the link has expired, false otherwise.</returns>
        public static bool IsEmailLinkExpired(DateTime creationTime)
        {
            DateTime expirationTime = creationTime.Add(emailTokenExpirationTime);
            return expirationTime < DateTime.UtcNow;
        }

        /// <summary>
        /// Verifies if the provided token matches the expected hash, using the provided salt.
        /// </summary>
        /// <param name="token">The token to verify.</param>
        /// <param name="salt">The salt used in hashing.</param>
        /// <param name="receivedHash">The hash to compare against.</param>
        /// <returns>True if the token is correct, false otherwise.</returns>
        public static bool IsCorrectToken(string token, string salt, string receivedHash)
        {
            var hashedToken = Rfc2898DeriveBytes.Pbkdf2(
                Convert.FromHexString(token),
                Convert.FromHexString(salt),
                resetTokenIterations,
                hashAlgorithm,
                resetTokenSize);

            return CryptographicOperations.FixedTimeEquals(hashedToken, Convert.FromHexString(receivedHash));
        }
    }
}
