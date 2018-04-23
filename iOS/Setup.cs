using System;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Platform;
using UIKit;

namespace PeterService.iOS
{
    public class Setup : MvxIosSetup
    {
        public Setup(MvxApplicationDelegate appDelegate, UIWindow window)
            : base (appDelegate, window)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new App();
        }
    }
}
