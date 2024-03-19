using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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
    public sealed partial class MortgageCalc : Page
    {
        public MortgageCalc()
        {
            this.InitializeComponent();
        }

		private void ExitButton_Click(object sender, RoutedEventArgs e)
		{
			this.Frame.Navigate(typeof(MainMenu));
		}

		private async void CalculateButton_Click(object sender, RoutedEventArgs e)
		{
			/*
			 * M = P [ i(1 + i)^n ] / [ (1 + i)^n – 1]
				P = principal loan amount
				i = monthly interest rate
				n = number of months required to repay the loan
				*/
			double p;
			double yearlyInterest;
			double j;
			int n;
			double i;
			double M;

			try
			{
				p = double.Parse(principalLoanAmountBox.Text);
			}
			catch (Exception)
			{
				var dialogMessage = new MessageDialog("Error please enter with only numbers. ");
				await dialogMessage.ShowAsync();
				principalLoanAmountBox.Focus(FocusState.Programmatic);
				principalLoanAmountBox.SelectAll();
				return;
			}
			try
			{
				yearlyInterest = double.Parse(yearlyInterestRateBox.Text);
			}
			catch (Exception)
			{
				var dialogMessage = new MessageDialog("Error please enter with only numbers. ");
				await dialogMessage.ShowAsync();
				yearlyInterestRateBox.Focus(FocusState.Programmatic);
				yearlyInterestRateBox.SelectAll();
				return;
			}
			j = (yearlyInterest / 12) / 100;
			monthlyInterestRateBox.Text = j.ToString("N4");
			try
			{
				n = int.Parse(monthsBox.Text) + (int.Parse(yearsBox.Text)*12);
			}
			catch (Exception)
			{
				var dialogMessage = new MessageDialog("Error please enter with only numbers. ");
				await dialogMessage.ShowAsync();
				yearsBox.Focus(FocusState.Programmatic);
				yearsBox.SelectAll();
				return;
			}
			try
			{
				i = double.Parse(monthlyInterestRateBox.Text);
			}
			catch (Exception)
			{
				var dialogMessage = new MessageDialog("Error please enter with only numbers. ");
				await dialogMessage.ShowAsync();
				monthlyInterestRateBox.Focus(FocusState.Programmatic);
				monthlyInterestRateBox.SelectAll();
				return;
			}
			double part1 = Math.Pow(i + 1, n);
			double part2 = Math.Pow(i + 1, n - 1);
			M = ((p*(i*(part1))) / (part2));
			monthlyPaymentBox.Text = M.ToString("C");
		}
	}
}
