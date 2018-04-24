using System;

using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using PeterService.ViewModels;
using UIKit;

namespace PeterService.iOS.Views
{
    public partial class HistoryTableCell : MvxTableViewCell
    {
        public static float CellHeight = 44f;
        public static readonly NSString Key = new NSString(nameof(HistoryTableCell));
        public static readonly UINib Nib;

        static HistoryTableCell()
        {
            Nib = UINib.FromName(nameof(HistoryTableCell), NSBundle.MainBundle);
        }

        protected HistoryTableCell(IntPtr handle) : base(handle)
        {
            this.DelayBind(() => {
                var set = this.CreateBindingSet<HistoryTableCell, HistoryCellViewModel>();
                set.Bind(HistoryCellLabel).To(vm => vm.Title);
                set.Apply();
            });
        }
    }
}
