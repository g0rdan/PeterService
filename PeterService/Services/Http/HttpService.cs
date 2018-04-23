using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PeterService.Services
{
    public class HttpService : IHttpService
    {
        readonly HttpClient _client;

        public HttpService()
        {
            _client = new HttpClient();
        }

        public async Task<HttpResponseMessage> Get(string url, CancellationToken token = default(CancellationToken))
        {
            try
            {
                using (var message = new HttpRequestMessage())
                {
                    message.Method = HttpMethod.Get;
                    message.RequestUri = new Uri(url);
                    return await _client.SendAsync(message, token).ConfigureAwait(false);
                }
            }
            catch (TaskCanceledException)
            {
                return new HttpResponseMessage(HttpStatusCode.ResetContent);
            }
            catch (WebException wEx)
            {
                return null;
            }
            catch (HttpRequestException rEx)
            {
                return null;
            }
        }
    }
}
