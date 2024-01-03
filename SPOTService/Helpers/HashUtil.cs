using System.Security.Cryptography;
using System.Text;

namespace SPOTService.Helpers
{
    public class HashUtil
    {
        public static string HashPassowd(string password) =>
            BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt());

        public static bool VerifyPassword(string password, string hashedPassword) =>
            BCrypt.Net.BCrypt.Verify(password, hashedPassword);

        public static string HashToken(string token) =>
            ComputeSHA256Hash(token);

        public static bool VerifyToken(string token, string hashedToken) =>
            ComputeSHA256Hash(token) == hashedToken;

        private static string ComputeSHA256Hash(string input)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = SHA256.HashData(inputBytes);

            StringBuilder sb = new();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("x2"));
            }

            return sb.ToString();
        }
    }
}
