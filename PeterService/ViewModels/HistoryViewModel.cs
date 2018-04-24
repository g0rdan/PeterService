using System.Linq;
using System.Threading.Tasks;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using PeterService.Services;
using PropertyChanged;

namespace PeterService.ViewModels
{
    public class HistoryViewModel : MvxViewModel
    {
        readonly IMvxNavigationService _navigationService;
        readonly IDataService _dataService;

        public MvxObservableCollection<HistoryCellViewModel> Items { get; set; }
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
            Items = new MvxObservableCollection<HistoryCellViewModel>();
		}

		public override Task Initialize()
		{
            var savedModels = _dataService.GetSavedResults(20);
            if (savedModels != null && savedModels.Any())
            {
                var cellVMs = savedModels
                    .Select(x => new HistoryCellViewModel { Title = $"{x.Original} -> {x.Definitions?.FirstOrDefault()?.Translates?.FirstOrDefault()?.Text}" });
                Items.AddRange(cellVMs);
            }

            return base.Initialize();
		}
	}
}
