using System;
using System.Collections.Generic;

namespace PeterService.Services
{
    public interface IDataService
    {
        List<TranslateModel> GetSavedResults(int count = 5);
        void Save(TranslateModel model);
    }
}
