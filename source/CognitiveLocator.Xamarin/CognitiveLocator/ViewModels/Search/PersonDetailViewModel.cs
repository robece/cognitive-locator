using CognitiveLocator.Domain;
using CognitiveLocator.Interfaces;

namespace CognitiveLocator.ViewModels
{
    public class PersonDetailViewModel : BaseViewModel
    {
        #region Properties

        private Person _currentPerson;

        public Person CurrentPerson
        {
            get { return _currentPerson; }
            set { SetProperty(ref _currentPerson, value); }
        }

        #endregion

        public PersonDetailViewModel(Person person) : base(new DependencyServiceBase())
        {
            Title = PersonDetail_Title;
            CurrentPerson = person;
        }

        #region Binding Multiculture

        public string PersonDetail_Title
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(PersonDetail_Title), Resx.AppResources.Culture); }
        }

        public string CreateReport_SectionPhoto
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_SectionPhoto), Resx.AppResources.Culture); }
        }

        public string CreateReport_Country
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_Country), Resx.AppResources.Culture); }
        }

        public string CreateReport_ReportedBy
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_ReportedBy), Resx.AppResources.Culture); }
        }

        public string CreateReport_Name
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_Name), Resx.AppResources.Culture); }
        }

        public string CreateReport_Lastname
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_Lastname), Resx.AppResources.Culture); }
        }

        public string CreateReport_LocationOfLoss
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_LocationOfLoss), Resx.AppResources.Culture); }
        }

        public string CreateReport_DateOfLoss
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_DateOfLoss), Resx.AppResources.Culture); }
        }

        public string CreateReport_ReportId
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_ReportId), Resx.AppResources.Culture); }
        }

        public string CreateReport_PhysicalAttributes
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_PhysicalAttributes), Resx.AppResources.Culture); }
        }

        public string CreateReport_Genre
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_Genre), Resx.AppResources.Culture); }
        }

        public string CreateReport_Complexion
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_Complexion), Resx.AppResources.Culture); }
        }

        public string CreateReport_Skin
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_Skin), Resx.AppResources.Culture); }
        }

        public string CreateReport_Front
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_Front), Resx.AppResources.Culture); }
        }

        public string CreateReport_Mouth
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_Mouth), Resx.AppResources.Culture); }
        }

        public string CreateReport_Eyebrows
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_Eyebrows), Resx.AppResources.Culture); }
        }

        public string CreateReport_Age
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_Age), Resx.AppResources.Culture); }
        }

        public string CreateReport_Height
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_Height), Resx.AppResources.Culture); }
        }

        public string CreateReport_Face
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_Face), Resx.AppResources.Culture); }
        }

        public string CreateReport_Nose
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_Nose), Resx.AppResources.Culture); }
        }

        public string CreateReport_Lips
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_Lips), Resx.AppResources.Culture); }
        }

        public string CreateReport_Chin
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_Chin), Resx.AppResources.Culture); }
        }

        public string CreateReport_TypeColorEyes
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_TypeColorEyes), Resx.AppResources.Culture); }
        }

        public string CreateReport_TypeColorHair
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_TypeColorHair), Resx.AppResources.Culture); }
        }

        public string CreateReport_SectionAditionalInformation
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_SectionAditionalInformation), Resx.AppResources.Culture); }
        }

        public string CreateReport_ParticularSigns
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_ParticularSigns), Resx.AppResources.Culture); }
        }

        public string CreateReport_Clothes
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_Clothes), Resx.AppResources.Culture); }
        }

        #endregion
    }
}