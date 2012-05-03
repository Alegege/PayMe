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
using Microsoft.Phone.Shell;

namespace PayMe
{
    public partial class NewPayMePage : PhoneApplicationPage
    {
        public NewPayMePage()
        {
            InitializeComponent();
			
			createApplicationBar();
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
				App.PayMeList.PayMes.Add(new PayMeItemViewModel(this.TitleInput.Text, 10, Convert.ToDouble(this.AmountInput.Text)));
			}
		}
		
		private void createApplicationBar()
        {
            //Create the ApplicationBar
            ApplicationBar = new ApplicationBar();
            ApplicationBar.IsVisible = true;
            ApplicationBar.IsMenuEnabled = true;
            //Create the icon buttons and setting its properties
            ApplicationBarIconButton newButton = new ApplicationBarIconButton(new Uri
            ("/icons/appbar.add.rest.png", UriKind.Relative));
            newButton.Text = "new";
            newButton.Click += new EventHandler(newButton_Click);
            ApplicationBarIconButton settingsButton = new ApplicationBarIconButton(new Uri
            ("/icons/appbar.feature.settings.rest.png", UriKind.Relative));
            settingsButton.Text = "settings";
            settingsButton.Click += new EventHandler(settingsButton_Click);
            //Add the icon buttons to the Application Bar
            ApplicationBar.Buttons.Add(newButton);
            ApplicationBar.Buttons.Add(settingsButton);
            //Create menu items
            ApplicationBarMenuItem settingsMenuItem = new ApplicationBarMenuItem
            ("about");
            settingsMenuItem.Click += new EventHandler(aboutMenuItem_Click);
            //Add the menu items to the Application Bar
            ApplicationBar.MenuItems.Add(settingsMenuItem);
        }
		
		private void newButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/NewPayMePage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Button 2 works!");
            //Do work for your application here.
        }

        private void aboutMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Button 3 works!");
            //Do work for your application here.
        }
    }
}
