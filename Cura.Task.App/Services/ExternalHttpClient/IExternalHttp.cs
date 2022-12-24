using Cura.Task.App.ViewModel;

namespace Cura.Task.App.Services.ExternalHttpClient
{
    public interface IExternalHttp
    {
        Task<T> GetAsync<T>(string url);
        Task<Tout> PostAsync<Tout, Tin>(string url, Tin input);
        Task<Tout> PostWithoutBodyAsync<Tout>(string url);
    }
}
