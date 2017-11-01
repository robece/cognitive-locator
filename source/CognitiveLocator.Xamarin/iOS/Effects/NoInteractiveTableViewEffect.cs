using CognitiveLocator.iOS.Effects;
using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("CognitiveLocator")]
[assembly: ExportEffect(typeof(NoInteractiveTableViewEffect), "NoInteractiveTableViewEffect")]
namespace CognitiveLocator.iOS.Effects
{
    public class NoInteractiveTableViewEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            if (Control == null)
                return;
            
            ((UITableView)Control).SeparatorStyle = UITableViewCellSeparatorStyle.None;
            ((UITableView)Control).AllowsSelection = false;
        }

        protected override void OnDetached()
        {
        }
    }
}