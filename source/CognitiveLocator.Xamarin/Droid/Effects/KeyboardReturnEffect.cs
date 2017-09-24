using System;
using CognitiveLocator.Droid.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportEffect(typeof(KeyboardReturnEffect), "KeyboardReturnEffect")]
namespace CognitiveLocator.Droid.Effects
{
    public class KeyboardReturnEffect : PlatformEffect
    {
		protected override void OnAttached()
		{
			if (Control == null)
				return;

           // var editText = Control as Android.Widget.EditText;
           
		}

		protected override void OnDetached()
		{
		}
    }
}
