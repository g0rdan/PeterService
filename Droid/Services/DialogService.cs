using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using MvvmCross.Platform;
using MvvmCross.Platform.Droid.Platform;
using PeterService.Services;

namespace PeterService.Droid.Services
{
    public class DialogService : IDialogService
    {
        string[] _items;

        public DialogService()
        {
        }

        public Action<string> ClickedOnItemAction { get; set; }

        public void ShowListOfItems(string title, IEnumerable<string> items)
        {
            _items = items.ToArray();
            var top = Mvx.Resolve<IMvxAndroidCurrentTopActivity>();
            var act = top.Activity;

            var adb = new AlertDialog.Builder(act);
            adb.SetTitle(title);
            adb.SetItems(_items, (sender, e) => {
                ClickedOnItemAction?.Invoke(_items[e.Which]);
            });

            using (var dialog = adb.Create())
            {
                dialog.Show();    
            }
        }
    }
}
