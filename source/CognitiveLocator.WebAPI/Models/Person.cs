using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CognitiveLocator.WebAPI.Models
{
    public class Person
    {
        public int IdPerson { get; set; }
        public int IsFound { get; set; }
        public string NameAlias { get; set; }/// Alias
        public int Age { get; set; }
        public string Picture { get; set; }
        public string Location { get; set; }
        public System.Data.Entity.Spatial.DbGeography GeoLocation { get; set; }
        public int IdHospital { get; set; }// Se va
        public string Notes { get; set; }
        public string Source { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public int IsActive { get; set; }
    }
}