using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using PeterService.Models;
using Plugin.Settings;

namespace PeterService.Services
{
    /// <summary>
    /// The class works with "preferences" on each platform
    /// Data serializes into string and deserializes as needed object
    /// </summary>
    public class DataService : IDataService
    {
        const string KEY = "peterservice";

        public DataService()
        {
        }

        public List<TranslateModel> GetSavedResults(int count = 5)
        {
            var currentStorageObjectString = CrossSettings.Current.GetValueOrDefault<string>(KEY, string.Empty);
            if (!string.IsNullOrWhiteSpace(currentStorageObjectString))
            {
                var translateModels = JsonConvert.DeserializeObject<List<TranslateModel>>(currentStorageObjectString);
                if (translateModels != null && translateModels.Any())
                    return translateModels.Take(count).Reverse().ToList();
            }
            return new List<TranslateModel>();
        }

        public void Save(TranslateModel model)
        {
            var translateModels = new List<TranslateModel>();
            var currentStorageObjectString = CrossSettings.Current.GetValueOrDefault<string>(KEY, string.Empty);
            if (!string.IsNullOrWhiteSpace(currentStorageObjectString))
                translateModels = JsonConvert.DeserializeObject<List<TranslateModel>>(currentStorageObjectString);
            
            translateModels.Add(model);
            CrossSettings.Current.AddOrUpdateValue<string>(KEY, JsonConvert.SerializeObject(translateModels));
        }
    }
}
