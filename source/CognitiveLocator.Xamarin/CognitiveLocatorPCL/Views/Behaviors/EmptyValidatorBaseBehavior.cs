using Xamarin.Forms;

namespace CognitiveLocator.Views.Behaviors
{
    public class EmptyValidatorBaseBehavior : Behavior<Entry>
    {
        private static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(EmptyValidatorBaseBehavior), false);

        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

        public bool IsValid
        {
            get { return (bool)base.GetValue(IsValidProperty); }
            private set { base.SetValue(IsValidPropertyKey, value); }
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(bindable);
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            IsValid = !string.IsNullOrEmpty(args.NewTextValue.Trim());
            SetSetting(IsValid);
        }

        public virtual void SetSetting(bool value)
        {
        }
    }
}