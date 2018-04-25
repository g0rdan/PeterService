using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using PeterService.ViewModels;

namespace PeterService.iOS.Views
{
    public partial class MainView : MvxViewController<MainViewModel>
    {
        public MainView() : base("MainView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            NavigationItem.Title = "Tralslator4000";

            var set = this.CreateBindingSet<MainView, MainViewModel>();
            set.Bind(InputTextView).To(vm => vm.InputText);
            set.Bind(OutputLabel).To(vm => vm.TranslatedText);
            set.Bind(FromButton).To(vm => vm.OpenFromListCommand);
            set.Bind(ToButton).To(vm => vm.OpenToListCommand);
            set.Bind(FromButton).For("Title").To(vm => vm.LanguageFromText);
            set.Bind(ToButton).For("Title").To(vm => vm.LanguageToText);
            set.Bind(HistoryButton).To(vm => vm.OpenHistoryCommand);
            set.Bind(TranslateButton).To(vm => vm.TranslateCommand);
            set.Apply();
        }
    }
}

