using System;
using System.Threading.Tasks;
using Akavache;
using System.Reactive.Linq;

namespace CognitiveLocator.Helpers
{
    public class AkavacheHelper
    {
        public static async Task InsertUserAccountObject<T>(string key, T value)
        {
            await BlobCache.UserAccount.InsertObject(key, value);
        }

        public static async Task<T> GetUserAccountObject<T>(string key)
        {
            T result = default(T);

            try
            {
                result = await BlobCache.UserAccount.GetObject<T>(key);
            }
            catch
            {
                result = default(T);
            }
            return result;
        }
    }
}
