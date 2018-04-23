using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PeterService.Services.Api
{
    public interface IApiService
    {
        Task<List<string>> GetLangs();
        Task<LangResultModel> Lookup(string text, string lang);
    }
}
