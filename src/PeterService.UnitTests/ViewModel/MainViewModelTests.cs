using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using MvvmCross.Core.Navigation;
using NUnit.Framework;
using PeterService.Models;
using PeterService.Services;
using PeterService.ViewModels;

namespace PeterService.UnitTests
{
    [TestFixture()]
    public class MainViewModelTests
    {
        readonly Mock<IMvxNavigationService> _navigationServiceMock;
        readonly Mock<IApiService> _apiServiceMock;
        readonly Mock<IDataService> _dataServiceMock;
        readonly Mock<IDialogService> _dialogService;
        readonly Mock<IHttpService> _httpService;

        public MainViewModelTests()
        {
            _navigationServiceMock = new Mock<IMvxNavigationService>();
            _apiServiceMock = new Mock<IApiService>();
            _dataServiceMock = new Mock<IDataService>();
            _dialogService = new Mock<IDialogService>();
            _httpService = new Mock<IHttpService>();

            _apiServiceMock.Setup(x => x.Lookup(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(new TranslateApiResult
            {
                OK = true,
                Result = new TranslateModel
                {
                    Original = "test",
                    Definitions = new List<Definition> { new Definition { Translates = new List<FullTranslate> { new FullTranslate { Text = "испытание" } } } }
                }
            });

            _httpService.Setup(x => x.HasConnection).Returns(true);
        }

        [Test]
        public async Task CheckCorrectTranslate()
        {
            var mainVM = new MainViewModel(
                _navigationServiceMock.Object,
                _apiServiceMock.Object,
                _dataServiceMock.Object,
                _dialogService.Object,
                _httpService.Object
            );

            mainVM.InputText = "test";
            mainVM.LangCodeFrom = "en";
            mainVM.LangCodeTo = "ru";
            await mainVM.TranslateCommand?.ExecuteAsync();
            Assert.True(mainVM.TranslatedText == "испытание");
        }

        [Test]
        public async Task CheckIncorrectSetupTranslate()
        {
            var mainVM = new MainViewModel(
                _navigationServiceMock.Object,
                _apiServiceMock.Object,
                _dataServiceMock.Object,
                _dialogService.Object,
                _httpService.Object
            );

            mainVM.InputText = "test";
            mainVM.LangCodeFrom = "en";
            // don't set up "lang to" for the case
            await mainVM.TranslateCommand?.ExecuteAsync();
            Assert.True(string.IsNullOrWhiteSpace(mainVM.TranslatedText));
        }

        [Test]
        public async Task CheckEmptyTextTranslate()
        {
            var mainVM = new MainViewModel(
                _navigationServiceMock.Object,
                _apiServiceMock.Object,
                _dataServiceMock.Object,
                _dialogService.Object,
                _httpService.Object
            );

            mainVM.InputText = string.Empty;
            mainVM.LangCodeFrom = "en";
            mainVM.LangCodeTo = "ru";
            await mainVM.TranslateCommand?.ExecuteAsync();
            Assert.True(string.IsNullOrWhiteSpace(mainVM.TranslatedText));
        }
    }
}
