using System;
using CognitiveLocator.ViewModels;

namespace CognitiveLocator.Views.Behaviors
{
    public class EmptyValidatorBehaviorName: EmptyValidatorBaseBehavior
    {
        public override void SetSetting(bool value)
        {
            SearchPersonViewModel.NameValidation = value;
        }
    }
}
