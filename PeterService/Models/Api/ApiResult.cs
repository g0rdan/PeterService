using System;
using PeterService.Services;

namespace PeterService.Models
{
    public class ApiResult
    {
        public bool OK { get; set; }
        public LangResultModel Result { get; set; }

        public ApiResult()
        {
        }

        public ApiResult(bool ok, LangResultModel result)
        {
            OK = ok;
            Result = result;
        }
    }
}
