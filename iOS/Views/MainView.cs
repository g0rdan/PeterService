using System;
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

            var set = this.CreateBindingSet<MainView, MainViewModel>();
            set.Bind(InputTextView).To(vm => vm.InputText);
            //set.Bind(OutputTextView).To(vm => vm.TranslatedText);
            set.Bind(OutputLabel).To(vm => vm.TranslatedText);
            set.Bind(DirectionButton).To(vm => vm.SelectDirectionCommand);
            set.Bind(DirectionButton).For("Title").To(vm => vm.LangDirection);
            set.Bind(HistoryButton).To(vm => vm.OpenHistoryCommand);
            set.Bind(TranslateButton).To(vm => vm.TranslateCommand);
            set.Apply();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

