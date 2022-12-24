using Cura.Task.App.Helper;
using Cura.Task.App.ViewModel;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace Cura.Task.App.Services.ExternalHttpClient
{
    public class ExternalHttp : IExternalHttp
    {
        private readonly IHttpClientFactory _httpClient;

        public ExternalHttp(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<T> GetAsync<T>(string url)
        {
            try
            {
                var httpClient = _httpClient.CreateClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", SessionData.SessionName);
                httpClient.DefaultRequestHeaders.Add("UserEmail", SessionData.UserEmail);
                var httpResponseMessage = await httpClient.GetAsync(url);
                var responseContentString = await httpResponseMessage.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<T>(responseContentString);
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<Tout> PostAsync<Tout, Tin>(string url, Tin input)
        {
            try
            {
                var httpClient = _httpClient.CreateClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", SessionData.SessionName);
                httpClient.DefaultRequestHeaders
                    .Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                  
                    
                httpClient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("en"));
 

                HttpClientHandler clientHandler = new HttpClientHandler();
               
               
                HttpClient client = new HttpClient(clientHandler);
            
                var httpResponseMessage = await httpClient.PostAsync(url, GetStringContent(input));
                var responseContentString = await httpResponseMessage.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Tout>(responseContentString);
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Tout> PostWithoutBodyAsync<Tout>(string url)
        {
            try
            {
                var httpClient = _httpClient.CreateClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", SessionData.SessionName);
                httpClient.DefaultRequestHeaders
                    .Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("en"));
                var httpResponseMessage = await httpClient.PostAsync(url, null);
                var responseContentString = await httpResponseMessage.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Tout>(responseContentString);
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private StringContent GetStringContent(object input)
        {
            var inputSerilized = JsonConvert.SerializeObject(input);

            var stringContent = new StringContent(inputSerilized, Encoding.UTF8, "application/json");

            return stringContent;
        }
    }
}
