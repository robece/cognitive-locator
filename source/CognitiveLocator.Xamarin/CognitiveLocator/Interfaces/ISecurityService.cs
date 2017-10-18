namespace CognitiveLocator.Interfaces
{
    public interface ISecurityService
    {
        string Encrypt(string clearValue, string encryptionKey);

        string Decrypt(string encryptedValue, string encryptionKey);
    }
}