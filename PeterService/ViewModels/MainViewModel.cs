using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using PeterService.Models;
using PeterService.Services;

namespace PeterService.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        const string BUTTON_FROM_TEMPLATE = "Язык оригинала";
        const string BUTTON_TO_TEMPLATE = "Язык перевода";

        readonly IMvxNavigationService _navigationService;
        readonly IApiService _apiService;
        readonly IDataService _dataService;
        readonly IMvxMessenger _messenger;
        List<LangDirectionModel> _langOptions;
        CancellationTokenSource _apiTokenSource;

        string _langCodeFrom;
        public string LangCodeFrom 
        {
            get { return _langCodeFrom; }
            set
            {
                _langCodeFrom = value;
                LanguageFromText = $"{BUTTON_FROM_TEMPLATE}: {value}";
            }
        }

        string _langCodeTo;
        public string LangCodeTo
        {
            get { return _langCodeTo; }
            set
            {
                _langCodeTo = value;
                LanguageToText = $"{BUTTON_TO_TEMPLATE}: {value}";
            }
        }

        public string InputText { get; set; }
        public string TranslatedText { get; set; }
        public string LanguageFromText { get; set; } = BUTTON_FROM_TEMPLATE;
        public string LanguageToText { get; set; } = BUTTON_TO_TEMPLATE;

        public IMvxAsyncCommand TranslateCommand => new MvxAsyncCommand(Translate);
        public IMvxCommand OpenFromListCommand => new MvxCommand(OpenFromList);
        public IMvxCommand OpenToListCommand => new MvxCommand(OpenToList);
        public IMvxAsyncCommand OpenHistoryCommand => new MvxAsyncCommand(OpenHistoryViewModel);

        public MainViewModel(
            IMvxNavigationService navigationService, 
            IApiService apiService, 
            IDataService dataService, 
            IMvxMessenger messenger
        )
        {
            _navigationService = navigationService;
            _apiService = apiService;
            _dataService = dataService;
            _messenger = messenger;
        }

        public async override Task Initialize()
		{
            _langOptions = await CreateLangOptions();
		}

		async Task Translate()
        {
            _apiTokenSource?.Cancel();
            _apiTokenSource = new CancellationTokenSource();

            var langDirection = GetLangDirection();
            if (!string.IsNullOrWhiteSpace(InputText) && !string.IsNullOrWhiteSpace(langDirection))
            {
                var result = await _apiService.Lookup(InputText, langDirection, _apiTokenSource.Token);
                if (result.OK)
                {
                    // save corrent translate into storage
                    _dataService.Save(result.Result);
                    // display the first corrent answer
                    TranslatedText = result.Result.Definitions?.FirstOrDefault()?.Translates?.FirstOrDefault()?.Text;
                }
            }
        }

        async Task OpenHistoryViewModel()
        {
            await _navigationService.Navigate<HistoryViewModel>();
        }

        void OpenFromList()
        {
             if (_langOptions != null && _langOptions.Any())
                _messenger.Publish(new OpenFromListMessage(this, _langOptions.Select(x => x.Key)));
        }

        void OpenToList()
        {
            if (!string.IsNullOrWhiteSpace(LangCodeFrom))
            {
                var givenOptions = _langOptions.FirstOrDefault(x => x.Key.ToLower() == LangCodeFrom.ToLower());
                if (givenOptions != null)
                    _messenger.Publish(new OpenToListMessage(this, givenOptions.Values));
            }
        }

        async Task<List<LangDirectionModel>> CreateLangOptions()
        {
            var result = new List<LangDirectionModel>();
            var itemsResult = await _apiService.GetLangs();
            if (itemsResult.OK)
            {
                var sptittedItems = itemsResult.Combinations.Select(x => x.Split('-'));
                var groupedItems = sptittedItems.GroupBy(x => x[0]);
                foreach (var item in groupedItems)
                {
                    var lgItem = new LangDirectionModel();
                    lgItem.Key = item.Key;
                    foreach (var lng in item)
                    {
                        lgItem.Values.Add(lng[1]);
                    }
                    result.Add(lgItem);
                }
            }
            return result;
        }

        string GetLangDirection()
        {
            if (!string.IsNullOrWhiteSpace(LangCodeFrom) && !string.IsNullOrWhiteSpace(LangCodeTo))
                return $"{LangCodeFrom.ToLower()}-{LangCodeTo.ToLower()}";
            return string.Empty;
        }
    }
}
