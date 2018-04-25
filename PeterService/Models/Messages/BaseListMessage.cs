using System;
using System.Collections.Generic;
using System.Linq;
using MvvmCross.Plugins.Messenger;

namespace PeterService.Models
{
    public class BaseListMessage : MvxMessage
    {
        public List<string> Items { get; set; }

        public BaseListMessage(object sender, IEnumerable<string> items) : base(sender)
        {
            Items = items.ToList();
        }
    }
}
