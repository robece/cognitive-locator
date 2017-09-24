using System;
namespace CognitiveLocator.Models.ApiModels
{
    public class CreateReportModel
    {
        public string Name
        {
            get;
            set;
        }

        public string LastName
        {
            get;
            set;
        }

        public string Alias
        {
            get;
            set;
        }

        public string Age
        {
            get;
            set;
        }

        public string Location
        {
            get;
            set;
        }

        public string Notes
        {
            get;
            set;
        }

        public string UrlFormat()
        {
            string result = string.Empty;

            result = "IsFound={0}";

            if (!string.IsNullOrEmpty(Name))
                result += string.Format("&Name={0}",Name);

            if (!string.IsNullOrEmpty(LastName))
                result += string.Format("&LastName={0}", LastName);

            if (!string.IsNullOrEmpty(Alias))
                result += string.Format("&Alias={0}", Alias);
            
            if (!string.IsNullOrEmpty(Age))
                result += string.Format("&Age={0}", Age);

            if (!string.IsNullOrEmpty(Location))
                result += string.Format("&Location={0}", Location);

            if (!string.IsNullOrEmpty(Notes))
                result += string.Format("&Notes={0}", Notes);

            return result;
        }

    }
}
