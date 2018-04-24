// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace PeterService.iOS.Views
{
    [Register ("MainView")]
    partial class MainView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton DirectionButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton HistoryButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField InputTextView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel OutputLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton TranslateButton { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (DirectionButton != null) {
                DirectionButton.Dispose ();
                DirectionButton = null;
            }

            if (HistoryButton != null) {
                HistoryButton.Dispose ();
                HistoryButton = null;
            }

            if (InputTextView != null) {
                InputTextView.Dispose ();
                InputTextView = null;
            }

            if (OutputLabel != null) {
                OutputLabel.Dispose ();
                OutputLabel = null;
            }

            if (TranslateButton != null) {
                TranslateButton.Dispose ();
                TranslateButton = null;
            }
        }
    }
}