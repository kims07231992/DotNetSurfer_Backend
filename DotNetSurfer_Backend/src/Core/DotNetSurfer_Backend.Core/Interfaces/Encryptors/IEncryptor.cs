namespace DotNetSurfer_Backend.Core.Interfaces.Encryptors
{
    public interface IEncryptor
    {
        bool IsEqual(string value, string encryptedValue);
        string Encrypt(string value);
    }
}
