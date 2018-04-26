using System;
using MvvmCross.Platform.Logging;

namespace PeterService.Services
{
    public class LogerService : ILogerService
    {
        readonly IMvxLog _loger;

        public LogerService(IMvxLog loger)
        {
            _loger = loger;
        }

        public void Debug(string message)
        {
            _loger.Debug(message);
        }

        public void Debug(Exception ex, string message = null)
        {
            _loger.Debug(ex, string.IsNullOrEmpty(message) ? ex.Message : message);
        }
    }
}
