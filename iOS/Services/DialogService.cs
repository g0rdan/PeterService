using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PeterService.Services;
using UIKit;

namespace PeterService.iOS.Services
{
    public class DialogService : IDialogService
    {
        public DialogService()
        {
        }

        public void Alert(string message, string title = null)
        {
            using (var actionSheetAlert = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert))
            {
                actionSheetAlert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
                GetCurrentViewController().PresentViewController(actionSheetAlert, true, null);
            }
        }

        public Task<DialogAnswer> ShowListOfItems(string title, IEnumerable<string> items)
        {
            var completionSource = new TaskCompletionSource<DialogAnswer>();

            if (items?.Any() ?? false)
            {
                using (var actionSheetAlert = UIAlertController.Create(title, string.Empty, UIAlertControllerStyle.ActionSheet))
                {
                    foreach (var item in items)
                    {
                        actionSheetAlert.AddAction(UIAlertAction.Create(item, UIAlertActionStyle.Default, (action) => {
                            var result = new DialogAnswer(true, item);
                            completionSource.SetResult(result);
                        }));
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

            return completionSource.Task;
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
