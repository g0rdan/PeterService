using System;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.iOS.Views;
using PeterService.ViewModels;
using UIKit;

namespace PeterService.iOS.Views
{
    public partial class HistoryView : MvxViewController<HistoryViewModel>
    {
        HistoryTableSource _tableSource;

        public HistoryView() : base("HistoryView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _tableSource = new HistoryTableSource(TableView, HistoryTableCell.Key, HistoryTableCell.Key);
            TableView.Source = _tableSource;
            TableView.TableFooterView = new UIView();

            var set = this.CreateBindingSet<HistoryView, HistoryViewModel>();
            set.Bind(_tableSource).To(vm => vm.Items).Apply();
        }
    }
}

