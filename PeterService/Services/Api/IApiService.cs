using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PeterService.Models;

namespace PeterService.Services
{
    public interface IApiService
    {
        Task<LangsApiResult> GetLangs(CancellationToken token = default(CancellationToken));
        Task<TranslateApiResult> Lookup(string text, string lang, CancellationToken token = default(CancellationToken));
    }
}
