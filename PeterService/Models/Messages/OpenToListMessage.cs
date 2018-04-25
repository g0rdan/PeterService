using System;
using System.Collections.Generic;
using System.Linq;
using MvvmCross.Plugins.Messenger;

namespace PeterService.Models
{
    public class OpenToListMessage : BaseListMessage
    {
        public OpenToListMessage(object sender, IEnumerable<string> items) : base(sender, items)
        {
        }
    }
}
