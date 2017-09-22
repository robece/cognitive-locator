using System;
namespace CognitiveLocator.Models
{
    public class Person
    {

		public int IdPerson { get; set; }
		public int IsFound { get; set; }
		public string NameAlias { get; set; }/// Alias
		public int Age { get; set; }
		public string Picture { get; set; }
		public string Location { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
		public string Notes { get; set; }
		public string Source { get; set; }
		public System.DateTime CreatedDate { get; set; }
		public System.DateTime ModifiedDate { get; set; }
		public int IsActive { get; set; }


        // TODO: Blob Storage
        public string PhotoURL
        {
            get;
            set;
        }
	}
}
