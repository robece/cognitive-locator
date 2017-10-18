using CognitiveLocator.iOS.Effects;
using System;
using UIKit;

[assembly: ResolutionGroupName("CognitiveLocator")]
[assembly: ExportEffect(typeof(NoInteractiveTableViewEffect), "NoInteractiveTableViewEffect")]

namespace CognitiveLocator.iOS.Effects
{
    public class NoInteractiveTableViewEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                ((UITableView)Control).SeparatorStyle = UITableViewCellSeparatorStyle.None;
                ((UITableView)Control).AllowsSelection = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot set property on attached control. Error: ", ex.Message);
            }
        }

        protected override void OnDetached()
        {
        }
    }
}