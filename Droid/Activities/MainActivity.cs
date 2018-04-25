﻿using Android.App;
using Android.Widget;
using Android.OS;
using PeterService.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace PeterService.Droid.Activities
{
    [Activity(Label = "Translator4000", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : MvxAppCompatActivity<MainViewModel>
    {
        int count = 1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);
        }
    }
}

