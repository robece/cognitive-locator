using CognitiveLocator.Domain;

namespace CognitiveLocator.Requests
{
    public class BaseRequest
    {
        public string Token { get; set; }
    }

    public class MetadataVerificationRequest : BaseRequest
    {
        public MetadataVerification Metadata { get; set; }
    }

    public class MobileSettingsRequest : BaseRequest
    {
    }
}