using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PeterService.Models;

namespace PeterService.Services
{
    public interface IApiService
    {
        /// <summary>
        /// Gets varialibility of translated langs
        /// Docs: https://tech.yandex.ru/dictionary/doc/dg/reference/getLangs-docpage/
        Task<LangsApiResult> GetLangs(CancellationToken token = default(CancellationToken));
        /// <summary>
        /// Gets JSON with translate particular text
        /// Docs: https://tech.yandex.ru/dictionary/doc/dg/reference/lookup-docpage/
        /// </summary>
        Task<TranslateApiResult> Lookup(string text, string lang, CancellationToken token = default(CancellationToken));
    }
}
