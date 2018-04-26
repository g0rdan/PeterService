using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using MvvmCross.Platform;
using MvvmCross.Platform.Droid.Platform;
using PeterService.Services;

namespace PeterService.Droid.Services
{
    public class DialogService : IDialogService
    {
        public DialogService()
        {
        }

        public void Alert(string message, string title = null)
        {
            var top = Mvx.Resolve<IMvxAndroidCurrentTopActivity>();
            var act = top.Activity;
            var adb = new AlertDialog.Builder(act);
            if (!string.IsNullOrWhiteSpace(title))
                adb.SetTitle(title);
            adb.SetMessage(message);
            adb.SetPositiveButton("Ok", (sender, e) => { });

            using (var dialog = adb.Create())
            {
                dialog.Show();
            }
        }

        public async Task<DialogAnswer> ShowListOfItems(string title, IEnumerable<string> items)
        {
            bool hasCompleted = false;
            var completionSource = new TaskCompletionSource<DialogAnswer>();
            var itemsArray = items.ToArray();
            var top = Mvx.Resolve<IMvxAndroidCurrentTopActivity>();
            var act = top.Activity;

            var adb = new AlertDialog.Builder(act);
            adb.SetTitle(title);
            adb.SetItems(itemsArray, (sender, e) => {
                hasCompleted = true;
                var result = new DialogAnswer(true, itemsArray[e.Which]);
                completionSource.SetResult(result);
            });

            using (var dialog = adb.Create())
            {
                dialog.Show();
                dialog.DismissEvent += (sender, e) => {
                    if(!hasCompleted)
                        completionSource.SetResult(new DialogAnswer());
                };
            }

            return await completionSource.Task;
        }
    }
}
