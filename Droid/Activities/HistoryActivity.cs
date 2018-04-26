
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Support.V7.RecyclerView;
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

            var recycleview = FindViewById<MvxRecyclerView>(Resource.Id.recycleview);
            recycleview.AddItemDecoration(new DividerItemDecoration(this, DividerItemDecoration.Vertical));
        }
    }
}
