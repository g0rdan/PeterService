using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PeterService.Models;

namespace PeterService.Services
{
    public class ApiService : IApiService
    {
        const string KEY = "dict.1.1.20180423T132850Z.947c7ac5e86d3c2d.3e6f3702acb8ff3e1d335680293544fa65846e40";
        const string ROUTE_BASE = "https://dictionary.yandex.net/api/v1/dicservice.json";
        readonly IHttpService _httpService;
        readonly ILogerService _logger;

        public ApiService(IHttpService httpService, ILogerService logger)
        {
            _httpService = httpService;
            _logger = logger;
        }

        public async Task<LangsApiResult> GetLangs(CancellationToken token = default(CancellationToken))
        {
            try
            {
                var query = $"{ROUTE_BASE}/getLangs?key={KEY}";
                var response = await _httpService.Get(query);
                if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<string>>(data);
                    if (result != null && result.Any())
                    {
                        return new LangsApiResult{ OK = true, Combinations = result };
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Debug(ex, ex.Message);
                return new LangsApiResult { Message = ex.Message };
            }

            return new LangsApiResult { Message = "Запрос не выполнился" };
        }

        public async Task<TranslateApiResult> Lookup(string text, string lang, CancellationToken token = default(CancellationToken))
        {
            try
            {
                var query = $"{ROUTE_BASE}/lookup?key={KEY}&lang={lang}&text={text}";
                var response = await _httpService.Get(query);
                if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var langResult = JsonConvert.DeserializeObject<TranslateModel>(data);
                    langResult.Original = text;
                    return new TranslateApiResult { OK = true, Result = langResult };
                }
            }
            catch (Exception ex)
            {
                _logger.Debug(ex, ex.Message);
                return new TranslateApiResult { Message = ex.Message };
            }

            return new TranslateApiResult { Message = "Запрос не выполнился" };
        }
    }
}
