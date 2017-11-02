using System;
namespace CognitiveLocator.Domain
{
    public class LanguageConfiguration
    {
        public LanguageConfiguration(string language)
        {
            this.Language = language;
        }

        public string Language
        {
            get;
            set;
        }
    }
}
