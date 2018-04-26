using Moq;
using NUnit.Framework;
using PeterService.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PeterService.UnitTests
{
    [TestFixture()]
    public class ApiServiceTests
    {
        readonly Mock<ILogerService> _logerServiceMock;

        public ApiServiceTests()
        {
            _logerServiceMock = new Mock<ILogerService>();
        }

        [Test()]
        public async Task CheckGettingLangs()
        {
            var httpService = new HttpService(_logerServiceMock.Object);
            var apiService = new ApiService(httpService, _logerServiceMock.Object);
            var result = await apiService.GetLangs();
            Assert.True(result.OK);
            Assert.True(result.Combinations.Count > 0);
        }

        [Test()]
        public async Task CheckGettingTranslate()
        {
            var originalText = "test";
            var direction = "en-ru";
            var httpService = new HttpService(_logerServiceMock.Object);
            var apiService = new ApiService(httpService, _logerServiceMock.Object);
            var result = await apiService.Lookup(originalText, direction);
            Assert.True(result.OK);
            var translatedText = result.Result.Definitions?.FirstOrDefault()?.Translates?.FirstOrDefault()?.Text;
            Assert.True(translatedText == "испытание");
        }
    }
}
