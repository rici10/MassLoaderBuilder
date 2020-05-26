namespace MassLoaderBuilder
{
    #region References

    using System;
    using System.Text;

    #endregion

    /*
        Created by r3xq1
        https://github.com/r3xq1/
        Telegram: https://t.me/r3xq1
    */

    public static class EncryptKey
    {
        /// <summary>
        /// Метод для шифрования строки
        /// </summary>
        /// <param name="input">Входные данные</param>
        /// <param name="key">Ключ</param>
        /// <returns>Зашифрованная строка</returns>
        public static string Encrypt(string input, string key)
        {
            string result = string.Empty;
            if (string.IsNullOrWhiteSpace(input)) return result;

            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                for (int i = 0; i < bytes.Length; i++)
                    bytes[i] = (byte)(bytes[i] ^ key[i % key.Length]);
                result = $"#r3{Convert.ToBase64String(bytes)}";
            }
            catch (Exception ex) { throw new Exception("Ошибка шифрования текста: ", ex); }
            return result;
        }

        /// <summary>
        /// Метод для расшифрования строки
        /// </summary>
        /// <param name="input">Входные данные</param>
        /// <param name="key">Ключ</param>
        /// <returns>Расшифрованная строка</returns>
        public static string Decrypt(string input, string key)
        {
            string Result = string.Empty;
            if (!input.StartsWith("#r3") && string.IsNullOrWhiteSpace(input)) return Result;

            try
            {
                input = input.Replace("#r3", "");
                byte[] bytes = Convert.FromBase64String(input);
                for (int i = 0; i < bytes.Length; i++)
                    bytes[i] = (byte)(bytes[i] ^ key[i % key.Length]);
                Result = Encoding.UTF8.GetString(bytes);
            }
            catch (Exception ex) { throw new Exception("Ошибка расшифровки текста: ", ex); }
            return Result;
        }
    }
}
