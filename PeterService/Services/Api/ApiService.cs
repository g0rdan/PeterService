﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MvvmCross.Platform.Logging;
using Newtonsoft.Json;
using PeterService.Models;

namespace PeterService.Services
{
    public class ApiService : IApiService
    {
        const string KEY = "dict.1.1.20180423T132850Z.947c7ac5e86d3c2d.3e6f3702acb8ff3e1d335680293544fa65846e40";
        const string ROUTE = "https://dictionary.yandex.net/api/v1/dicservice.json";
        readonly IHttpService _httpService;
        readonly IMvxLog _logger;

        public ApiService(IHttpService httpService, IMvxLog logger)
        {
            _httpService = httpService;
            _logger = logger;
        }

        public async Task<LangsApiResult> GetLangs(CancellationToken token = default(CancellationToken))
        {
            try
            {
                var query = $"{ROUTE}/getLangs?key={KEY}";
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
                return new LangsApiResult();
            }

            return new LangsApiResult();
        }

        public async Task<TranslateApiResult> Lookup(string text, string lang, CancellationToken token = default(CancellationToken))
        {
            try
            {
                var query = $"{ROUTE}/lookup?key={KEY}&lang={lang}&text={text}";
                var response = await _httpService.Get(query);
                if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var langResult = JsonConvert.DeserializeObject<LangResultModel>(data);
                    return new TranslateApiResult { OK = true, Result = langResult };
                }
            }
            catch (Exception ex)
            {
                _logger.Debug(ex, ex.Message);
                return new TranslateApiResult();
            }

            return new TranslateApiResult();
        }
    }
}