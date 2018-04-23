using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PeterService.Models;

namespace PeterService.Services
{
    public class ApiService : IApiService
    {
        const string KEY = "dict.1.1.20180423T132850Z.947c7ac5e86d3c2d.3e6f3702acb8ff3e1d335680293544fa65846e40";
        readonly IHttpService _httpService;

        public ApiService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public Task<List<string>> GetLangs()
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResult> Lookup(string text, string lang)
        {
            try
            {
                var query = $"https://dictionary.yandex.net/api/v1/dicservice.json/lookup?key={KEY}&lang={lang}&text={text}";
                var response = await _httpService.Get(query);
                if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var langResult = JsonConvert.DeserializeObject<LangResultModel>(data);
                    return new ApiResult (true, langResult);
                }
            }
            catch (Exception ex)
            {
                return new ApiResult();
            }

            return new ApiResult();
        }
    }
}
