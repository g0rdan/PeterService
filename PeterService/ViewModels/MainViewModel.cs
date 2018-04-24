using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using PeterService.Services;
using PropertyChanged;

namespace PeterService.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        CancellationTokenSource _tokenSource;
        readonly IMvxNavigationService _navigationService;
        readonly IApiService _apiService;
        readonly IDataService _dataService;

        public string LangDirection { get; set; } = "ru-en";
        public string InputText { get; set; }
        public string TranslatedText { get; set; }

        public IMvxAsyncCommand TranslateCommand => new MvxAsyncCommand(Translate);
        public IMvxAsyncCommand SelectDirectionCommand => new MvxAsyncCommand(OpenLangDirectionViewModel);
        public IMvxAsyncCommand OpenHistoryCommand => new MvxAsyncCommand(OpenHistoryViewModel);

        public MainViewModel(IMvxNavigationService navigationService, IApiService apiService, IDataService dataService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            _dataService = dataService;
        }

		async Task Translate()
        {
            //if (string.IsNullOrWhiteSpace(LangDirection))
                

            _tokenSource?.Cancel();
            _tokenSource = new CancellationTokenSource();

            if (!string.IsNullOrWhiteSpace(InputText) && !string.IsNullOrWhiteSpace(LangDirection))
            {
                var result = await _apiService.Lookup(InputText, LangDirection, _tokenSource.Token);
                if (result.OK)
                {
                    // save corrent translate into storage
                    _dataService.Save(result.Result);
                    // display the first corrent answer
                    TranslatedText = result.Result.Definitions?.FirstOrDefault()?.Translates?.FirstOrDefault()?.Text;
                }
            }
        }

        async Task OpenLangDirectionViewModel()
        {
            var result = await _navigationService.Navigate<LangDirectionViewModel, string, string>(LangDirection);
            if (!string.IsNullOrWhiteSpace(result))
                LangDirection = result;
        }

        async Task OpenHistoryViewModel()
        {
            await _navigationService.Navigate<HistoryViewModel>();
        }
	}
}
