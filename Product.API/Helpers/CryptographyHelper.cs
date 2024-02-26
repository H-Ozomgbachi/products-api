namespace Product.API.Helpers
{
    public class CryptographyHelper
    {
        private readonly AppSettings _appSettings;

        public CryptographyHelper(IOptions<AppSettings> options)
        {
            _appSettings = options.Value;
        }
        public string EncryptString(params string[] plainText)
        {
            using Aes aes = Aes.Create();
            aes.Key = Convert.FromBase64String(_appSettings.EKey);
            aes.IV = Convert.FromBase64String(_appSettings.EIv);

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using MemoryStream memoryStream = new();
            using CryptoStream cryptoStream = new(memoryStream, encryptor, CryptoStreamMode.Write);

            byte[] plainBytes = Encoding.ASCII.GetBytes(string.Join('|', plainText));
            cryptoStream.Write(plainBytes, 0, plainBytes.Length);
            cryptoStream.FlushFinalBlock();

            byte[] cipherBytes = memoryStream.ToArray();
            return Convert.ToBase64String(cipherBytes);
        }
        public string DecryptString(string cipherText)
        {
            using Aes aes = Aes.Create();
            aes.Key = Convert.FromBase64String(_appSettings.EKey);
            aes.IV = Convert.FromBase64String(_appSettings.EIv);

            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using MemoryStream memoryStream = new();
            using CryptoStream cryptoStream = new(memoryStream, decryptor, CryptoStreamMode.Write);

            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            cryptoStream.Write(cipherBytes, 0, cipherBytes.Length);
            cryptoStream.FlushFinalBlock();

            byte[] plainBytes = memoryStream.ToArray();
            return Encoding.ASCII.GetString(plainBytes);
        }
        public string HashValue(params string[] values)
        {
            string input = string.Join("|", values);
            using SHA256 sHA = SHA256.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = sHA.ComputeHash(inputBytes);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }
}

