using System;
using System.Linq;
using System.Threading.Tasks;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using PeterService.Services;

namespace PeterService.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        readonly IMvxNavigationService _navigationService;
        readonly IApiService _apiService;
        readonly IDataService _dataService;

        public string LangDirection { get; set; }
        public string InputText { get; set; }
        public string OutputText { get; set; }

        public IMvxAsyncCommand TranslateCommand => new MvxAsyncCommand(Translate);
        public IMvxAsyncCommand SelectDirectionCommand => new MvxAsyncCommand(OpenLangDirectionViewModel);

        public MainViewModel(IMvxNavigationService navigationService, IApiService apiService, IDataService dataService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            _dataService = dataService;
        }

        async Task OpenLangDirectionViewModel()
        {
            var result = await _navigationService.Navigate<LangDirectionViewModel, string, string>(LangDirection);
            if (!string.IsNullOrWhiteSpace(result))
                LangDirection = result;
        }

		async Task Translate()
        {
            if (!string.IsNullOrWhiteSpace(InputText) && !string.IsNullOrWhiteSpace(LangDirection))
            {
                var result = await _apiService.Lookup(InputText, LangDirection);
                if (result.OK)
                {
                    // save corrent translate into storage
                    _dataService.Save(result.Result);
                    // display first corrent answer
                    OutputText = result.Result.Definitions?.FirstOrDefault()?.Translates?.FirstOrDefault()?.Text;
                }
            }
        }
	}
}
