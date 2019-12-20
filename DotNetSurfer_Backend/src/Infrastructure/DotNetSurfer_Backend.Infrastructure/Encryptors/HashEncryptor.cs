using DotNetSurfer_Backend.Core.Interfaces.Encryptors;
using System.Security.Cryptography;
using System.Text;

namespace DotNetSurfer_Backend.Infrastructure.Encryptors
{
    public class HashEncryptor : IEncryptor
    {
        public bool IsEqual(string value, string encryptedValue)
        {
            return Encrypt(value) == encryptedValue;
        }

        public string Encrypt(string str)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            var sha1 = SHA1.Create();
            byte[] hash = sha1.ComputeHash(bytes);
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
