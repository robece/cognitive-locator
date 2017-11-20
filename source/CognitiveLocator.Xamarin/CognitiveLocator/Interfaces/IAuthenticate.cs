using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace CognitiveLocator.Interfaces
{
    public interface IAuthenticateService
    {
        void GetUserInfo();

        string GetToken();

        bool IsAuthenticated();

        Task<string> Authenticate(JObject token);

        Task ClearToken();
    }
}