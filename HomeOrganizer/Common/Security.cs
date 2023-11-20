﻿using System.Security.Cryptography;
using System.Text;

using HomeOrganizer.Models.User;

namespace HomeOrganizer.Common
{
    public static class Security
    {
        private static readonly int keySize = 64;
        private static readonly int iterations = 350000;
        private static readonly HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

        private static readonly int resetTokenSize = 32;
        private static readonly int resetTokenIterations = 500000;

        public static readonly TimeSpan tokenExpirationTime = new TimeSpan(0, 10, 0);

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

        public static bool IsLinkExpired(DateTime creationTime)
        {
            DateTime expirationTime = creationTime.Add(tokenExpirationTime);

            return expirationTime < DateTime.UtcNow;
        }

        public static bool IsCorrectToken(string token, ResetPasswordData resetData)
        {
            var hashedToken = Rfc2898DeriveBytes.Pbkdf2(
                Convert.FromHexString(token),
                Convert.FromHexString(resetData.Salt),
                resetTokenIterations,
                hashAlgorithm,
                resetTokenSize);

            return CryptographicOperations.FixedTimeEquals(hashedToken, Convert.FromHexString(resetData.HashedToken));
        }
    }
}
