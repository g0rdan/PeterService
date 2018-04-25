
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V7.AppCompat;
using PeterService.ViewModels;

namespace PeterService.Droid.Activities
{
    [Activity(Label = "History")]
    public class HistoryActivity : MvxAppCompatActivity<HistoryViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.History);
        }
    }
}
