using System;
using CognitiveLocator.ViewModels;

namespace CognitiveLocator.Views.Behaviors
{
    public class EmptyValidatorBehaviorLastName: EmptyValidatorBaseBehavior
    {
        public override void SetSetting(bool value)
        {
            SearchPersonViewModel.LastNameValidation = value;
        }
    }
}
