using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CognitiveLocator.WebAPI.Models
{
    public class Person
    {
        public string IdPerson { get; set; }
        public int IsFound { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Alias { get; set; }
        public DateTime? BirthDate { get; set; }
        public string ReportedBy { get; set; }
        public string Picture { get; set; }
        public string Location { get; set; }
        public string Notes { get; set; }
        public int IsActive { get; set; }
        public string FaceId { get; set; }
    }
}