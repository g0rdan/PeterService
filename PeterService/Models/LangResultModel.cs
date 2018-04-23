using System.Collections.Generic;
using Newtonsoft.Json;

namespace PeterService.Services
{
    public class LangResultModel
    {
        [JsonProperty("head")]
        public Head Head { get; set; }
        public List<Definition> Definitions { get; set; }
    }

    public class Head
    {
    }

    public class Synonym
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }

    public class Meaninig
    {
        [JsonProperty("text")]
        public string text { get; set; }
    }

    public class Example
    {
        [JsonProperty("syn")]
        public string Text { get; set; }
        [JsonProperty("syn")]
        public List<Translate> Translates { get; set; }
    }

    public class Translate
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }

    public class FullTranslate
    {
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("pos")]
        public string PartOfSpeech { get; set; }
        [JsonProperty("syn")]
        public List<Synonym> Synonyms { get; set; }
        [JsonProperty("mean")]
        public List<Meaninig> Means { get; set; }
        [JsonProperty("ex")]
        public List<Example> Examples { get; set; }
    }

    public class Definition
    {
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("pos")]
        public string PartOfSpeech { get; set; }
        [JsonProperty("tr")]
        public List<FullTranslate> Translates { get; set; }
    }
}