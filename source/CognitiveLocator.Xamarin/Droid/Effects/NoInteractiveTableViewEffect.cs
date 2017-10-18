using System;
using Xamarin.Forms.Platform.Android;
using CognitiveLocator.Droid.Effects;
using Xamarin.Forms;
using Android.Graphics.Drawables;

[assembly: ResolutionGroupName("CognitiveLocator")]
[assembly: ExportEffect(typeof(NoInteractiveTableViewEffect), "NoInteractiveTableViewEffect")]
namespace CognitiveLocator.Droid.Effects
{
    public class NoInteractiveTableViewEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            if (Control == null)
                return;

            var listView = Control as Android.Widget.ListView;
            listView.DividerHeight = 0;
            listView.Divider = new ColorDrawable(Android.Graphics.Color.Transparent);
            listView.SetSelector(Android.Resource.Color.Transparent);
        }

        protected override void OnDetached()
        {
        }
    }
}