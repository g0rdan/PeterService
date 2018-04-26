using System;
using System.Collections.Generic;
using System.Linq;

namespace PeterService.Models
{
    public class LangsApiResult : BaseApiResult
    {
        public List<string> Combinations { get; set; }
    }
}
