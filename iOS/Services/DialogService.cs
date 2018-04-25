using System;
using System.Collections.Generic;
using System.Linq;
using PeterService.Services;
using UIKit;

namespace PeterService.iOS.Services
{
    public class DialogService : IDialogService
    {
        public DialogService()
        {
        }

        public Action<string> ClickedOnItemAction { get; set; }

        public void ShowListOfItems(string title, IEnumerable<string> items)
        {
            if (items?.Any() ?? false)
            {
                var actionSheetAlert = UIAlertController.Create(title, string.Empty, UIAlertControllerStyle.ActionSheet);
                foreach (var item in items)
                {
                    actionSheetAlert.AddAction(UIAlertAction.Create(item, UIAlertActionStyle.Default, (action) => ClickedOnItemAction?.Invoke(item)));
                }
                var presentationPopover = actionSheetAlert.PopoverPresentationController;
                if (presentationPopover != null)
                {
                    presentationPopover.SourceView = GetCurrentViewController().View;
                    presentationPopover.PermittedArrowDirections = UIPopoverArrowDirection.Up;
                }
                GetCurrentViewController().PresentViewController(actionSheetAlert, true, null);
            }
        }

        UIViewController GetCurrentViewController()
        {
            var window = UIApplication.SharedApplication.KeyWindow;
            var vc = window.RootViewController;
            while (vc.PresentedViewController != null)
            {
                vc = vc.PresentedViewController;
            }
            return vc;
        }
    }
}
