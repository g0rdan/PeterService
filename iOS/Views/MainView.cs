using System;
using System.Linq;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;
using PeterService.Models;
using PeterService.ViewModels;
using UIKit;

namespace PeterService.iOS.Views
{
    public partial class MainView : MvxViewController<MainViewModel>
    {
        MvxSubscriptionToken _fromToken;
        MvxSubscriptionToken _toToken;

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

		public override void ViewWillAppear(bool animated)
		{
            base.ViewWillAppear(animated);
            _fromToken = Mvx.Resolve<IMvxMessenger>().Subscribe<OpenFromListMessage>(ShowPopupList);
            _toToken = Mvx.Resolve<IMvxMessenger>().Subscribe<OpenToListMessage>(ShowPopupList);
		}

		public override void ViewWillDisappear(bool animated)
		{
			base.ViewWillDisappear(animated);
            Mvx.Resolve<IMvxMessenger>().Unsubscribe<OpenFromListMessage>(_fromToken);
            Mvx.Resolve<IMvxMessenger>().Unsubscribe<OpenToListMessage>(_toToken);
		}

        void ShowPopupList(BaseListMessage message)
        {
            if (message.Items?.Any() ?? false)
            {
                var title = message is OpenFromListMessage ? "Выберите язык оригинала:" : "Выберите язык перевода:";
                var actionSheetAlert = UIAlertController.Create(title, string.Empty, UIAlertControllerStyle.ActionSheet);
                foreach (var item in message.Items)
                {
                    actionSheetAlert.AddAction(UIAlertAction.Create(item, UIAlertActionStyle.Default, (action) => SendItemToVM(message, item)));
                }
                var presentationPopover = actionSheetAlert.PopoverPresentationController;
                if (presentationPopover != null)
                {
                    presentationPopover.SourceView = View;
                    presentationPopover.PermittedArrowDirections = UIPopoverArrowDirection.Up;
                }
                this.PresentViewController(actionSheetAlert, true, null);
            }
        }

        void SendItemToVM(BaseListMessage message, string item)
        {
            if (message is OpenFromListMessage)
            {
                ViewModel.LangCodeFrom = item.ToLower();
            }
            else
            {
                ViewModel.LangCodeTo = item.ToLower();
            }
        }
    }
}

