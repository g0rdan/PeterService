using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PeterService.Services
{
    public interface IDialogService
    {
        /// <summary>
        /// Shows a simple alert window
        /// </summary>
        void Alert(string message, string title = null);
        /// <summary>
        /// Shows the list of items in popup window
        /// </summary>
        Task<DialogAnswer> ShowListOfItems(string title, IEnumerable<string> items);
    }

    public class DialogAnswer
    {
        public bool Ok { get; set; }
        public string SelectedItem { get; set; }

        public DialogAnswer()
        {
        }

        public DialogAnswer(bool ok, string selectedItem)
        {
            Ok = ok;
            SelectedItem = selectedItem;
        }
    }
}
