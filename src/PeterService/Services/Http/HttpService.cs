using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Plugin.Connectivity;

namespace PeterService.Services
{
    public class HttpService : IHttpService
    {
        readonly HttpClient _client;
        readonly ILogerService _logger;

        public HttpService(ILogerService logger)
        {
            _logger = logger;
            _client = new HttpClient();
        }

        public bool HasConnection => CrossConnectivity.Current.IsConnected;

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
            catch (TaskCanceledException tEx)
            {
                _logger.Debug(tEx, tEx.Message);
                return new HttpResponseMessage(HttpStatusCode.ResetContent);
            }
            catch (WebException wEx)
            {
                _logger.Debug(wEx, wEx.Message);
                return null;
            }
            catch (HttpRequestException rEx)
            {
                _logger.Debug(rEx, rEx.Message);
                return null;
            }
        }
    }
}
