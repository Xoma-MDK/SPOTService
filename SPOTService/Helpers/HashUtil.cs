using System.Security.Cryptography;
using System.Text;

namespace SPOTService.Helpers
{
    /// <summary>
    /// Утилитарный класс для хеширования паролей и токенов.
    /// </summary>
    public class HashUtil
    {
        /// <summary>
        /// Хеширование пароля с использованием bcrypt.
        /// </summary>
        /// <param name="password">Пароль для хеширования.</param>
        /// <returns>Хешированный пароль.</returns>
        public static string HashPassword(string password) =>
            BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt());

        /// <summary>
        /// Проверка соответствия пароля хешированному паролю.
        /// </summary>
        /// <param name="password">Не хешированный пароль.</param>
        /// <param name="hashedPassword">Хешированный пароль.</param>
        /// <returns>True, если пароль верен, иначе False.</returns>
        public static bool VerifyPassword(string password, string hashedPassword) =>
            BCrypt.Net.BCrypt.Verify(password, hashedPassword);

        /// <summary>
        /// Хеширование токена с использованием SHA-256.
        /// </summary>
        /// <param name="token">Токен для хеширования.</param>
        /// <returns>Хешированный токен.</returns>
        public static string HashToken(string token) =>
            ComputeSHA256Hash(token);

        /// <summary>
        /// Проверка соответствия токена хешированному токену.
        /// </summary>
        /// <param name="token">Не хешированный токен.</param>
        /// <param name="hashedToken">Хешированный токен.</param>
        /// <returns>True, если токены соответствуют, иначе False.</returns>
        public static bool VerifyToken(string token, string hashedToken) =>
            ComputeSHA256Hash(token) == hashedToken;

        /// <summary>
        /// Вычисление хеша SHA-256 для входных данных.
        /// </summary>
        /// <param name="input">Входные данные для хеширования.</param>
        /// <returns>Хеш SHA-256 в виде строки.</returns>
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
