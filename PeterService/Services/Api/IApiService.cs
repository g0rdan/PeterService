using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PeterService.Models;

namespace PeterService.Services
{
    public interface IApiService
    {
        Task<List<string>> GetLangs();
        Task<ApiResult> Lookup(string text, string lang);
    }
}
