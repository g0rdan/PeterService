// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace PeterService.iOS.Views
{
    [Register ("HistoryTableCell")]
    partial class HistoryTableCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel HistoryCellLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (HistoryCellLabel != null) {
                HistoryCellLabel.Dispose ();
                HistoryCellLabel = null;
            }
        }
    }
}