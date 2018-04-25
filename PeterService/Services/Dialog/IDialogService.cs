using System;
using System.Collections.Generic;

namespace PeterService.Services
{
    public interface IDialogService
    {
        /// <summary>
        /// Executes when we're clicking on item in popup window
        /// </summary>
        Action<string> ClickedOnItemAction { get; set; }
        /// <summary>
        /// Shows the list of items in popup window
        /// </summary>
        void ShowListOfItems(string title, IEnumerable<string> items);
    }
}
