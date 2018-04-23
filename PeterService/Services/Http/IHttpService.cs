using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PeterService.Services
{
    public interface IHttpService
    {
        Task<HttpResponseMessage> Get(string url, CancellationToken token = default(CancellationToken));
    }
}
