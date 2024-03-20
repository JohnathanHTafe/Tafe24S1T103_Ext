using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238


namespace Calculator
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class CurrencyConverter : Page
	{
		private Dictionary<string, Dictionary<string, double>> conversionRates = new Dictionary<string, Dictionary<string, double>>();

		public CurrencyConverter()
		{
			this.InitializeComponent();

			// Initialize and populate the conversionRates dictionary
			InitializeConversionRates();
		}

		private void InitializeConversionRates()
		{
			// Conversion rates from USD
			conversionRates["US Dollar"] = new Dictionary<string, double>
			{
				{"Euro", 0.85189982 },
				{"British Pound", 0.72872436 },
				{"Indian Rupee", 74.257327 }
			};

			// Conversion rates from Euro
			conversionRates["Euro"] = new Dictionary<string, double>
			{
				{"US Dollar", 1.1739732 },
				{"British Pound", 0.8556672 },
				{"Indian Rupee", 87.00755 }
			};

			// Conversion rates from British Pound
			conversionRates["British Pound"] = new Dictionary<string, double>
			{
				{"US Dollar", 1.371907 },
				{"Euro", 1.1686692 },
				{"Indian Rupee", 101.68635 }
			};

			// Conversion rates from Indian Rupee
			conversionRates["Indian Rupee"] = new Dictionary<string, double>
			{
				{"US Dollar", 0.011492628 },
				{"Euro", 0.013492774 },
				{"British Pound", 0.0098339397 }
			};
		}

		private async void ConversionButton_Click(object sender, RoutedEventArgs e)
		{
			string fromCurrency = (fromCurrencyComboBox.SelectedItem as ComboBoxItem).Content.ToString();
			string toCurrency = (toCurrencyComboBox.SelectedItem as ComboBoxItem).Content.ToString();

			if (fromCurrency == toCurrency)
			{
				var dialog = new Windows.UI.Popups.MessageDialog("You cannot convert to the same currency. Please select another currency to convert.");
				await dialog.ShowAsync();
				return;
			}

			double fromAmount;
			if (!double.TryParse(amountTextBox.Text, out fromAmount))
			{
				var dialog = new Windows.UI.Popups.MessageDialog("Invalid input. Please enter a valid number.");
				await dialog.ShowAsync();
				return;
			}

			double toAmount = ConvertCurrency(fromAmount, fromCurrency, toCurrency);

			outputAmountFromTextBlock.Text = $"{fromAmount} {fromCurrency} = ";
			outputAmountToTextBlock.Text = $"{toAmount:C8} {toCurrency}";
			fromConversionRateTextBlock.Text = $"1 {fromCurrency} = {conversionRates[fromCurrency][toCurrency]} {toCurrency}";
			toConversionRateTextBlock.Text = $"1 {toCurrency} = {conversionRates[toCurrency][fromCurrency]} {fromCurrency}";

			outputAmountToTextBlock.FontSize = 30;
		}

		private double ConvertCurrency(double amount, string fromCurrency, string toCurrency)
		{
			// Check if direct conversion rate exists
			if (conversionRates[fromCurrency].ContainsKey(toCurrency))
			{
				return amount * conversionRates[fromCurrency][toCurrency];
			}
			// Check if inverse conversion rate exists
			else if (conversionRates[toCurrency].ContainsKey(fromCurrency))
			{
				return amount / conversionRates[toCurrency][fromCurrency];
			}
			else
			{
				// Handle case where conversion rate is not found
				throw new ArgumentException("Conversion rate not found for the specified currencies.");
			}
		}

		private void exitButton_Click(object sender, RoutedEventArgs e)
		{
			this.Frame.Navigate(typeof(MainMenu));
		}
	}
}
