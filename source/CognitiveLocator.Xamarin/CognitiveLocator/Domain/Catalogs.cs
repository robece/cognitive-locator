using System;
using System.Collections.Generic;

namespace CognitiveLocator.Domain
{
    public class Catalogs
    {
        #region Languages

        private static Dictionary<string, string> languages = new Dictionary<string, string>();

        public static void InitLanguages()
        {
            languages.Clear();
            languages.Add("en-US", Catalogs_LanguageEnglish);
            languages.Add("es-MX", Catalogs_LanguageSpanish);
        }

        public static List<string> GetLanguages()
        {
            List<string> result = new List<string>();

            foreach (KeyValuePair<string, string> kv in languages)
                result.Add(kv.Value);

            return result;
        }

        public static string GetLanguageKey(string value)
        {
            string result = string.Empty;
            foreach (KeyValuePair<string, string> kv in languages)
                if (kv.Value == value) result = kv.Key;

            return result;
        }

        public static string GetLanguageValue(string key)
        {
            return languages[key];
        }

        public static int GetLanguageIndex(string key)
        {
            int result = 0;
            foreach (KeyValuePair<string, string> kv in languages)
            {
                if (kv.Key == key)
                    break;
                result++;
            }
            return result;
        }

        #endregion

        #region Country

        private static Dictionary<string, string> countries = new Dictionary<string, string>();

        public static void InitCountries()
        {
            countries.Clear();
            countries.Add("MX", "México");
        }

        public static List<string> GetCountries()
        {
            List<string> result = new List<string>();

            foreach (KeyValuePair<string, string> kv in countries)
                result.Add(kv.Value);

            return result;
        }

        public static string GetCountryKey(string value)
        {
            string result = string.Empty;
            foreach (KeyValuePair<string, string> kv in countries)
                if (kv.Value == value) result = kv.Key;

            return result;
        }

        public static string GetCountryValue(string key)
        {
            return countries[key];
        }

        #endregion

        #region Genre

        private static Dictionary<string, string> genre = new Dictionary<string, string>();

        public static void InitGenre()
        {
            genre.Clear();
            genre.Add("M", Catalogs_GenreMale);
            genre.Add("F", Catalogs_GenreFemale);
        }

        public static List<string> GetGenre()
        {
            List<string> result = new List<string>();

            foreach (KeyValuePair<string, string> kv in genre)
                result.Add(kv.Value);

            return result;
        }

        public static string GetGenreKey(string value)
        {
            string result = string.Empty;
            foreach (KeyValuePair<string, string> kv in genre)
                if (kv.Value == value) result = kv.Key;

            return result;
        }

        public static string GetGenreValue(string key)
        {
            return genre[key];
        }

        #endregion

        #region Binding Multiculture

        public static string Catalogs_LanguageSpanish
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(Catalogs_LanguageSpanish), Resx.AppResources.Culture); }
        }

        public static string Catalogs_LanguageEnglish
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(Catalogs_LanguageEnglish), Resx.AppResources.Culture); }
        }

        public static string Catalogs_GenreMale
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(Catalogs_GenreMale), Resx.AppResources.Culture); }
        }

        public static string Catalogs_GenreFemale
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(Catalogs_GenreFemale), Resx.AppResources.Culture); }
        }

        #endregion
    }
}
