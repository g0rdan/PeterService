using System.Linq;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using PeterService.Services;

namespace PeterService.ViewModels
{
    public class HistoryViewModel : MvxViewModel
    {
        readonly IMvxNavigationService _navigationService;
        readonly IDataService _dataService;

        public MvxObservableCollection<string> Items { get; set; }
        public IMvxAsyncCommand CloseViewModelCommand => new MvxAsyncCommand(async () => {
            await _navigationService.Close(this);
        });

        public HistoryViewModel(IMvxNavigationService navigationService, IDataService dataService)
        {
            _navigationService = navigationService;
            _dataService = dataService;
        }

		public override void Prepare()
		{
            var savedModels = _dataService.GetSavedResults();
            var savedStrings = savedModels.Select(x => $"{x.Original} -> {x.Definitions?.FirstOrDefault()?.Translates?.FirstOrDefault()?.Text}");
            Items = new MvxObservableCollection<string>(savedStrings);
		}
	}
}
