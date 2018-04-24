using System;
using System.Collections.Generic;

namespace PeterService.Services
{
    public interface IDataService
    {
        /// <summary>
        /// Gets latest saved results from "preferences"
        /// Gets last 5 items by default
        /// </summary>
        List<TranslateModel> GetSavedResults(int count = 5);
        /// <summary>
        /// Saves item in "preferences"
        /// </summary>
        void Save(TranslateModel model);
    }
}
