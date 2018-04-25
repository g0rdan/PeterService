using System;
using System.Collections.Generic;

namespace PeterService.Models
{
    public class OpenFromListMessage : BaseListMessage
    {
        public OpenFromListMessage(object sender, IEnumerable<string> items) : base(sender, items)
        {
        }
    }
}
