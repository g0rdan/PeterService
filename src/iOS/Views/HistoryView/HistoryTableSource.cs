using System;
using Foundation;
using MvvmCross.Binding.iOS.Views;
using UIKit;

namespace PeterService.iOS.Views
{
    public class HistoryTableSource : MvxSimpleTableViewSource
    {
        public HistoryTableSource(UITableView tableView, string nibName, string cellIdentifier)
            : base(tableView, nibName, cellIdentifier)
        {
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return HistoryTableCell.CellHeight;
        }
    }
}
