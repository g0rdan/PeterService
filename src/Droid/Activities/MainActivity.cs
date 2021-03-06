﻿using Android.App;
using Android.OS;
using PeterService.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;
using Android.Widget;
using Android.Views.Animations;
using Android.Views;
using System;
using Android.Support.Design.Widget;

namespace PeterService.Droid.Activities
{
    [Activity(Label = "Translator4000", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : MvxAppCompatActivity<MainViewModel>
    {
        FrameLayout _resultView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            var translateButton = FindViewById<FloatingActionButton>(Resource.Id.translateButton);
            translateButton.Click += (sender, e) => HideResult();
            _resultView = FindViewById<FrameLayout>(Resource.Id.result_view);
            ViewModel.TranslationHasCompleted = ShowResult;
        }

        void ShowResult()
        {
            var slideUp = AnimationUtils.LoadAnimation(this, Resource.Animation.slide_up);
            if (_resultView.Visibility == ViewStates.Invisible)
            {
                _resultView.StartAnimation(slideUp);
                _resultView.Visibility = ViewStates.Visible;
            }
        }

        void HideResult()
        {
            var slideDown = AnimationUtils.LoadAnimation(this, Resource.Animation.slide_down);
            if (_resultView.Visibility == ViewStates.Visible)
            {
                _resultView.StartAnimation(slideDown);
                _resultView.Visibility = ViewStates.Invisible;
            }
        }
    }
}

