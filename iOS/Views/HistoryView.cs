using System;
using MvvmCross.iOS.Views;
using PeterService.ViewModels;
using UIKit;

namespace PeterService.iOS.Views
{
    public partial class HistoryView : MvxViewController<HistoryViewModel>
    {
        public HistoryView() : base("HistoryView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

