using Newtonsoft.Json;
using System.Collections.Generic;

namespace CognitiveLocator.Domain.Templates
{
    public class Registration
    {
        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("sections")]
        public List<TableSection> Sections { get; set; } = new List<TableSection>();

        public class TableSection
        {
            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("elements")]
            public List<BaseElement> Elements { get; set; } = new List<BaseElement>();
        }

        public class BaseElement
        {
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("margin")]
            public string Margin { get; set; }

            [JsonProperty("textcolor")]
            public string TextColor { get; set; }
        }

        public class Label : BaseElement
        {
            [JsonProperty("fontsize")]
            public string FontSize { get; set; }

            [JsonProperty("text")]
            public string Text { get; set; }
        }

    }
}