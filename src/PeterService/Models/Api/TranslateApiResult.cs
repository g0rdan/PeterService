using System;
using PeterService.Services;

namespace PeterService.Models
{
    public class TranslateApiResult : BaseApiResult
    {
        public TranslateModel Result { get; set; }
    }
}
