using System;
using System.ComponentModel;
using Android.App;
using Android.Widget;
using Forms=Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using CognitiveLocator.Droid.Controls;

[assembly: ExportRenderer(typeof(CognitiveLocator.Views.Controls.NullableDatePicker), typeof(NullableDatePickerRenderer))]
namespace CognitiveLocator.Droid.Controls
{
    
	public class NullableDatePickerRenderer : ViewRenderer<Views.Controls.NullableDatePicker, EditText>
	{
		DatePickerDialog _dialog;
		protected override void OnElementChanged(ElementChangedEventArgs<Views.Controls.NullableDatePicker> e)
		{
			base.OnElementChanged(e);

            this.SetNativeControl(new Android.Widget.EditText(Forms.Forms.Context));
			if (Control == null || e.NewElement == null)
				return;

			this.Control.Click += OnPickerClick;
			this.Control.KeyListener = null;
			this.Control.FocusChange += OnPickerFocusChange;
			this.Control.Enabled = Element.IsEnabled;

			var nullableDatePicker = this.Element as Views.Controls.NullableDatePicker;
			this.Control.Hint = nullableDatePicker.Placeholder;
			this.Control.Text = "";
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

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
                            this.Control.Hint = nullableDatePicker.Placeholder;
							this.Control.Text = "";
						}
						break;
				}
			}
		}

		void OnPickerFocusChange(object sender, Android.Views.View.FocusChangeEventArgs e)
		{
			if (e.HasFocus)
			{
				ShowDatePicker();
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (Control != null)
			{
				this.Control.Click -= OnPickerClick;
				this.Control.FocusChange -= OnPickerFocusChange;

				if (_dialog != null)
				{
					_dialog.Hide();
					_dialog.Dispose();
					_dialog = null;
				}
			}

			base.Dispose(disposing);
		}

		void OnPickerClick(object sender, EventArgs e)
		{
			ShowDatePicker();
		}

		void SetDate(DateTime date)
		{
			this.Control.Text = date.ToString(Element.Format);
			Element.Date = date;
		}

		private void ShowDatePicker()
		{
			CreateDatePickerDialog(this.Element.Date.Year, this.Element.Date.Month - 1, this.Element.Date.Day);
			_dialog.Show();
		}

		void CreateDatePickerDialog(int year, int month, int day)
		{
			Views.Controls.NullableDatePicker view = Element;
			_dialog = new DatePickerDialog(Context, (o, e) =>
			{
				view.Date = e.Date;
				((Forms.IElementController)view).SetValueFromRenderer(Forms.VisualElement.IsFocusedProperty, false);
				Control.ClearFocus();

				_dialog = null;
			}, year, month, day);

			_dialog.SetButton("Elegir", (sender, e) =>
			{
				SetDate(_dialog.DatePicker.DateTime);
				this.Element.Format = this.Element._originalFormat;
				this.Element.AssignValue();
                Control.ClearFocus();
			});
			_dialog.SetButton2("Ninguna", (sender, e) =>
			{
				this.Element.CleanDate();
                Control.ClearFocus();
			});
		}
	}
}
