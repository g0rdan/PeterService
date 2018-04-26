using System;
namespace PeterService.Services
{
    public interface ILogerService
    {
        void Debug(string message);
        void Debug(Exception ex, string message = null);
    }
}
