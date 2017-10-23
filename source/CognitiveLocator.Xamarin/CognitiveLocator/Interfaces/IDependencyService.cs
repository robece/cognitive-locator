using Xamarin.Forms;

namespace CognitiveLocator.Interfaces
{
    public interface IDependencyService
    {
        T Get<T>() where T : class;
    }

    public class DependencyServiceBase : IDependencyService
    {
        public T Get<T>() where T : class
        {
            return DependencyService.Get<T>();
        }
    }
}