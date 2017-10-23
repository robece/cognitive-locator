using System;
using System.Collections.Generic;

namespace CognitiveLocator.Domain
{
    public class Catalogs
    {
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
            genre.Add("M", "Masculino");
            genre.Add("F", "Femenino");
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
    }
}
