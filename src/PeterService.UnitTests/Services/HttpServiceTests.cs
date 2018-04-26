using System;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using PeterService.Services;

namespace PeterService.UnitTests
{
    [TestFixture()]
    public class HttpServiceTests
    {
        readonly Mock<ILogerService> _loggerMock;

        public HttpServiceTests()
        {
            // we won't do setup for loger, just inizialization
            _loggerMock = new Mock<ILogerService>();
        }

        [Test()]
        public async Task CheckCorrectAnswerFromQuery()
        {
            var httpService = new HttpService(_loggerMock.Object);
            var result = await httpService.Get("http://yandex.ru");
            Assert.True(result.IsSuccessStatusCode);
        }

        [Test()]
        public async Task CheckIncorrectAnswerFromQuery()
        {
            var httpService = new HttpService(_loggerMock.Object);
            var result = await httpService.Get("http://sdfsgrgvrthvteyhvetyjvytjtyjbtjtyjn.ru");
            Assert.False(result?.IsSuccessStatusCode ?? false);
        }
    }
}
