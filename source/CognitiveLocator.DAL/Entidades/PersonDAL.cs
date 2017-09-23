using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveLocatorDAL.Entidades
{
    public class PersonDAL
    {
        public int IsFound { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Alias { get; set; }
        public int Age { get; set; }
        public string Picture { get; set; }
        public string Location { get; set; }
        public string Notes { get; set; }
        public int IsActive { get; set; }
        public float FaceId { get; set; }
        public float Height { get; set; }
        public float Width { get; set; }
        public float LeftMargin { get; set; }
        public float RightMargin { get; set; }
    }
}
