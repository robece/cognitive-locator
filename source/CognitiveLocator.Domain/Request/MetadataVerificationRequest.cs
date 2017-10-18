using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveLocator.Domain
{
    public class MetadataVerificationRequest : BaseRequest
    {
        public MetadataVerification Metadata { get; set; }
    }
}
