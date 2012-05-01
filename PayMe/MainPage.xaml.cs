using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Windows.Navigation;

namespace PayMe
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();			
            this.payMeList.DataContext = App.PayMeList;
            this.Loaded +=new RoutedEventHandler(MainPage_Loaded);
        }

        // Cargar datos para los elementos de ViewModel
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.PayMeList.IsDataLoaded)
            {
                App.PayMeList.LoadData();
            }
        }
		
		//The foreground color of the text in TitleInput is set to Magenta when TitleInput
		//gets focus.
		private void TextInputTitle_GotFocus(object sender, RoutedEventArgs e)
		{
			if (TitleInput.Text == "Title")
			{
				TitleInput.Text = "";
				TitleInput.Foreground = new SolidColorBrush(ApplicationConstants.inputTextColor);
			}
		}
		
		//The foreground color of the text in WatermarkTB is set to Blue when WatermarkTB
		//loses focus. Also, if TitleInput loses focus and no text is entered, the
		//text "Title" is displayed.
		private void TextInputTitle_LostFocus(object sender, RoutedEventArgs e)
		{
			if (TitleInput.Text == String.Empty)
			{
				TitleInput.Text = "Title";
				TitleInput.Foreground = new SolidColorBrush(ApplicationConstants.placeholderColor);
			}
		}
		
		//The foreground color of the text in AmountInput is set to Magenta when AmountInput
		//gets focus.
		private void TextInputAmount_GotFocus(object sender, RoutedEventArgs e)
		{
			if (AmountInput.Text == "Amount")
			{
				AmountInput.Text = "";
				AmountInput.Foreground = new SolidColorBrush(ApplicationConstants.inputTextColor);
			}
		}
		
		//The foreground color of the text in AmountInput is set to Blue when AmountInput
		//loses focus. Also, if AmountInput loses focus and no text is entered, the
		//text "Amount" is displayed.
		private void TextInputAmount_LostFocus(object sender, RoutedEventArgs e)
		{
			if (AmountInput.Text == String.Empty)
			{
				AmountInput.Text = "Amount";
				AmountInput.Foreground = new SolidColorBrush(ApplicationConstants.placeholderColor);
			}
		}
		
		private void CreatePayMe(object sender, RoutedEventArgs e) {
			if (this.TitleInput.Text != String.Empty &&
				this.AmountInput.Text != String.Empty)
			{
				App.PayMeList.PayMes.Add(new PayMeItemViewModel(this.TitleInput.Text, Convert.ToInt32(this.AmountInput.Text)));
			}
		}
    }
}