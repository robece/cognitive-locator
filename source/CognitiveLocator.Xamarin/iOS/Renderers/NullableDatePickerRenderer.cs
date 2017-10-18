using CognitiveLocator.iOS.Renderers;
using CognitiveLocator.Views.Controls;
using System;
using System.Collections.Generic;
using UIKit;

[assembly: ExportRenderer(typeof(NullableDatePicker), typeof(NullableDatePickerRenderer))]

namespace CognitiveLocator.iOS.Renderers
{
    public class NullableDatePickerRenderer : DatePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null && this.Control != null)
            {
                this.AddClearButton();

                var nullableDatePicker = this.Element as Views.Controls.NullableDatePicker;
                this.Control.Placeholder = nullableDatePicker.Placeholder;
                this.Control.Text = "";

                if (Device.Idiom == TargetIdiom.Tablet)
                {
                    this.Control.Font = UIFont.SystemFontOfSize(25);
                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (this.Control == null) return;

            var nullableDatePicker = this.Element as Views.Controls.NullableDatePicker;

            if (nullableDatePicker != null)
            {
                switch (e.PropertyName)
                {
                    case nameof(nullableDatePicker.NullableDate):
                        if (nullableDatePicker.NullableDate != null)
                            this.Control.Text = nullableDatePicker.NullableDate.Value.ToString(nullableDatePicker.Format);
                        else
                        {
                            this.Control.Placeholder = nullableDatePicker.Placeholder;
                            this.Control.Text = "";
                        }
                        break;
                }
            }
        }

        private void AddClearButton()
        {
            var originalToolbar = this.Control.InputAccessoryView as UIToolbar;

            if (originalToolbar != null && originalToolbar.Items.Length <= 2)
            {
                var clearButton = new UIBarButtonItem("Ninguna", UIBarButtonItemStyle.Plain, ((sender, ev) =>
                {
                    Views.Controls.NullableDatePicker baseDatePicker = this.Element as Views.Controls.NullableDatePicker;
                    this.Element.Unfocus();
                    this.Element.Date = DateTime.Now;
                    baseDatePicker.CleanDate();
                }));

                var newItems = new List<UIBarButtonItem>();
                foreach (var item in originalToolbar.Items)
                {
                    newItems.Add(item);
                }

                newItems.Insert(0, clearButton);

                originalToolbar.Items = newItems.ToArray();
                originalToolbar.SetNeedsDisplay();
            }
        }
    }
}