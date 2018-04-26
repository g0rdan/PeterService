using System;
using MvvmCross.Core.ViewModels;

namespace PeterService.ViewModels
{
    /// <summary>
    /// Represents a table cell in History Views
    /// </summary>
    public class HistoryCellViewModel : MvxViewModel
    {
        public string Title { get; set; }
    }
}
