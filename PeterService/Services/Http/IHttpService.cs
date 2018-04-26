using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PeterService.Services
{
    public interface IHttpService
    {
        /// <summary>
        /// Check do we have any internet connection or not
        /// </summary>
        bool HasConnection { get; }
        /// <summary>
        /// Simple GET query
        /// </summary>
        Task<HttpResponseMessage> Get(string url, CancellationToken token = default(CancellationToken));
    }
}
