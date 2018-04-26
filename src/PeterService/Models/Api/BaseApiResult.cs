using System;
namespace PeterService.Models
{
    public abstract class BaseApiResult
    {
        public bool OK { get; set; }
        public string Message { get; set; }
    }
}
