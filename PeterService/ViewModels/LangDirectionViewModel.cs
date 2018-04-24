using System.Threading.Tasks;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using PeterService.Services;
using PropertyChanged;

namespace PeterService.ViewModels
{
    public class LangDirectionViewModel : MvxViewModel<string, string>
    {
        readonly IApiService _apiService;
        readonly IMvxNavigationService _navigationService;

        public string Title { get; set; } = "Choose lang direction";
        public MvxObservableCollection<string> Items { get; set; }
        public IMvxAsyncCommand<string> SelectLangDirectionCommand => new MvxAsyncCommand<string>(SelectLangDirection);

        public LangDirectionViewModel(IApiService apiService, IMvxNavigationService navigationService)
        {
            _apiService = apiService;
            _navigationService = navigationService;
        }

        public override void Prepare(string parameter)
        {
            if (!string.IsNullOrWhiteSpace(parameter))
                Title = $"Current direction: {parameter}";
        }

        public async override Task Initialize()
		{
            var itemsResult = await _apiService.GetLangs();
            if (itemsResult.OK)
                Items = new MvxObservableCollection<string>(itemsResult.Combinations);
		}

        async Task SelectLangDirection(string item)
        {
            await _navigationService.Close(this, item);
        }
	}
}
