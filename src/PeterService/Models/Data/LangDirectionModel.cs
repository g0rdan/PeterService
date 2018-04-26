using System;
using System.Collections.Generic;

namespace PeterService.Models
{
    /// <summary>
    /// Stores options to choise propriate translate direction
    /// </summary>
    public class LangDirectionModel
    {
        public string Key { get; set; }
        public List<string> Values { get; set; }

        public LangDirectionModel()
        {
            Values = new List<string>();
        }
    }
}
