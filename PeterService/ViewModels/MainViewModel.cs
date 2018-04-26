using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
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
        readonly IDialogService _dialogService;
        readonly IHttpService _httpService;

        List<LangDirectionModel> _langOptions;
        CancellationTokenSource _apiTokenSource;

        string _langCodeFrom;
        /// <summary>
        /// Represents first part of "lang direction" (for example, it's "en" in "en-ru")
        /// </summary>
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
        /// <summary>
        /// Represents second part of "lang direction" (for example, it's "ru" in "en-ru")
        /// </summary>
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
        public IMvxAsyncCommand OpenFromListCommand => new MvxAsyncCommand(OpenFromList);
        public IMvxAsyncCommand OpenToListCommand => new MvxAsyncCommand(OpenToList);
        public IMvxAsyncCommand OpenHistoryCommand => new MvxAsyncCommand(OpenHistoryViewModel);

        public MainViewModel(
            IMvxNavigationService navigationService, 
            IApiService apiService, 
            IDataService dataService, 
            IDialogService dialogService,
            IHttpService httpService
        )
        {
            _navigationService = navigationService;
            _apiService = apiService;
            _dataService = dataService;
            _dialogService = dialogService;
            _httpService = httpService;
        }

        public async override Task Initialize()
		{
            if (_httpService.HasConnection)
            {
                _langOptions = await CreateLangOptions();
            }
            else
            {
                _dialogService.Alert("Для работы приложения нужно активное интернет соединение", "Предупреждение");
            }

        }

		async Task Translate()
        {
            _apiTokenSource?.Cancel();
            _apiTokenSource = new CancellationTokenSource();

            var langDirection = GetLangDirection();

            if (string.IsNullOrWhiteSpace(InputText))
            {
                _dialogService.Alert("Введите слово для перевода");
                return;
            }

            if (string.IsNullOrWhiteSpace(langDirection))
            {
                _dialogService.Alert("Не выбранно направление перевода");
                return;
            }
             
            var result = await _apiService.Lookup(InputText, langDirection, _apiTokenSource.Token);
            if (result.OK)
            {
                // save corrent translate into storage
                _dataService.Save(result.Result);
                // display the first corrent answer
                TranslatedText = result.Result.Definitions?.FirstOrDefault()?.Translates?.FirstOrDefault()?.Text;
                if (string.IsNullOrWhiteSpace(TranslatedText))
                    _dialogService.Alert("Подходящий перевод не найден");
            }
            else
            {
                _dialogService.Alert(result.Message);
            }
        }

        async Task OpenHistoryViewModel()
        {
            await _navigationService.Navigate<HistoryViewModel>();
        }

        async Task OpenFromList()
        {
            if (_langOptions != null && _langOptions.Any())
            {
                var answer = await _dialogService.ShowListOfItems("Выберите язык оригинала", _langOptions.Select(x => x.Key));
                if (answer.Ok)
                    LangCodeFrom = answer.SelectedItem;
            }
        }

        async Task OpenToList()
        {
            if (!string.IsNullOrWhiteSpace(LangCodeFrom))
            {
                var givenOptions = _langOptions.FirstOrDefault(x => x.Key.ToLower() == LangCodeFrom.ToLower());
                if (givenOptions != null)
                {
                    var answer = await _dialogService.ShowListOfItems("Выберите язык перевода", _langOptions.Select(x => x.Key));
                    if (answer.Ok)
                        LangCodeTo = answer.SelectedItem;
                }
            }
            else
            {
                _dialogService.Alert("Сначала выберите язык оригинала");
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
