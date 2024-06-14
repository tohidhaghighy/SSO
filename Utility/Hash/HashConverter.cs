using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace Utility.Hash
{
    public static class HashConverter
    {
        public static byte[] CreateSalt()
        {
            return RandomNumberGenerator.GetBytes(128 / 8);
        }

        public static string HashPass(string password)
        {
            // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password!,
                salt: new byte[] {},
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
        }

        public static bool CheckHash(string userEnteredPassword, string dbPasswordHash, string dbPasswordSalt)
        {
            string hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: userEnteredPassword!,
                salt: System.Convert.FromBase64String(dbPasswordSalt),
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
            return hash == dbPasswordHash;
        }
    }
}
