using System;
using Xamarin.Forms;

namespace CognitiveLocator.Pages.Behaviors
{
    public class NextEntryFocusBehavior : Behavior<Entry>
    {
        private readonly BindableProperty NextEntryProperty =
            BindableProperty.Create(nameof(NextEntry), typeof(Entry), typeof(NextEntryFocusBehavior), null);

        public Entry NextEntry
        {
            get { return (Entry)base.GetValue(NextEntryProperty); }
            set
            { base.SetValue(NextEntryProperty, value); }
        }

        private Entry attachedEntry;

        protected override void OnAttachedTo(Entry bindable)
        {
            attachedEntry = bindable;
            attachedEntry.Completed += AttachedEntry_Completed;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            attachedEntry.Completed -= AttachedEntry_Completed;
        }

        private void AttachedEntry_Completed(object sender, EventArgs e)
        {
            NextEntry?.Focus();
        }
    }
}