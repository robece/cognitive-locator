namespace CognitiveLocator.Services
{
    public interface IEmailService
    {
        /// <summary>
        /// Crea un correo usando apis nativas
        /// </summary>
        /// <param name="mail">Correo a excribir</param>
        void SendEmail(string mail);
    }
}
