using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using PeterService.ViewModels;
using UIKit;

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
            TranslateLabel.Hidden = true;
            OutputLabel.Hidden = true;

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

            TranslateButton.TouchUpInside += (sender, e) => HideResult();
            ViewModel.TranslationHasCompleted = ShowResult;
        }

        void TranslateButton_TouchUpInside(object sender, System.EventArgs e)
        {
        }

        void ShowResult()
        {
            TranslateLabel.Hidden = false;
            OutputLabel.Hidden = false;

            UIView.Animate(1, 0, UIViewAnimationOptions.CurveLinear,
                () => {
                    TranslateLabel.Alpha = 100f;
                    OutputLabel.Alpha = 100f;
                },
                () => {
                    // animation complete logic 
                }
            );
        }

        void HideResult()
        {
            UIView.Animate(1, 0, UIViewAnimationOptions.CurveLinear,
                () => {
                    TranslateLabel.Alpha = 0f;
                    OutputLabel.Alpha = 0f;
                },
                () => {
                    // animation complete logic 
                }
            );
        }
    }
}

