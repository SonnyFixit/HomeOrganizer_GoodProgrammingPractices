using System.Security.Cryptography;
using System.Text;

namespace HomeOrganizer.Common
{
    public static class Security
    {
        private static readonly int keySize = 64;
        private static readonly int iterations = 350000;
        private static readonly HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;


        public static string GenerateSalt()
        {
            byte[] salt = RandomNumberGenerator.GetBytes(keySize);
            return Convert.ToHexString(salt);
        }

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
    }
}
