using System;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;

namespace PeterService.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        public string InputText { get; set; }
        public string OutputText { get; set; }

        public IMvxAsyncCommand TranslateCommand => new MvxAsyncCommand(Translate);

        public MainViewModel()
        {
        }

        async Task Translate()
        {
            throw new NotImplementedException();
        }
	}
}
