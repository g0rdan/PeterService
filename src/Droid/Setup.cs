using System;
using Android.Content;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platform;
using PeterService.Droid.Services;
using PeterService.Services;

namespace PeterService.Droid
{
    public class Setup : MvxAppCompatSetup
    {
        public Setup(Context applicationContext) : base(applicationContext)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new App();
        }

		protected override void InitializeFirstChance()
		{
            Mvx.RegisterSingleton<IDialogService>(new DialogService());
            base.InitializeFirstChance();
		}
	}
}
