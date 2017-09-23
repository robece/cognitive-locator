using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CognitiveLocator.WebAPI.Models.FaceApiModel
{
    public class FindSimilar
    {
        public string persistedFaceId { get;set;}
        public float confidence {get; set;}

    }
}