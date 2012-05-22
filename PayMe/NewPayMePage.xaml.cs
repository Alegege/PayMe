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
using Microsoft.Phone.Tasks;
using System.Xml.Serialization;
using System.IO;
using System.IO.IsolatedStorage;
using System.Text.RegularExpressions;

namespace PayMe {
    public partial class NewPayMePage : PhoneApplicationPage {

        public ParticipantListViewModel contactList { get; private set; }

		private EmailAddressChooserTask emailTask;
		
        public NewPayMePage() {
            InitializeComponent();

            contactList = new ParticipantListViewModel();
            this.EmailList.DataContext = contactList;

			emailTask = new EmailAddressChooserTask();
    		emailTask.Completed += new EventHandler<EmailResult>(emailTask_Completed);
        }
		
		private void btnContacts_Click(object sender, RoutedEventArgs e) {
			try {	
				emailTask.Show();
			} catch (System.InvalidOperationException ex) {
				MessageBox.Show("An error occurred.");
			}				
		}
		
		void emailTask_Completed(object sender, EmailResult contact) {
			if (contact.TaskResult == TaskResult.OK) {
                if (!contactList.existsParticipant(contact)) {
                    contactList.addParticipant(contact);
				}				
			}
		}
		
		//The foreground color of the text in TitleInput is set to Magenta when TitleInput
		//gets focus.
		private void TextInputTitle_GotFocus(object sender, RoutedEventArgs e) {
			if (TitleInput.Text == "Title") {
				TitleInput.Text = "";
				TitleInput.Foreground = new SolidColorBrush(ApplicationConstants.inputTextColor);
			}
		}
		
		//The foreground color of the text in WatermarkTB is set to Blue when WatermarkTB
		//loses focus. Also, if TitleInput loses focus and no text is entered, the
		//text "Title" is displayed.
		private void TextInputTitle_LostFocus(object sender, RoutedEventArgs e) {
			if (TitleInput.Text == String.Empty) {
				TitleInput.Text = "Title";
				TitleInput.Foreground = new SolidColorBrush(ApplicationConstants.placeholderColor);
			}
		}
		
		//The foreground color of the text in AmountInput is set to Magenta when AmountInput
		//gets focus.
		private void TextInputAmount_GotFocus(object sender, RoutedEventArgs e) {
			if (AmountInput.Text == "Amount") {
				AmountInput.Text = "";
				AmountInput.Foreground = new SolidColorBrush(ApplicationConstants.inputTextColor);
			}
		}
		
		//The foreground color of the text in AmountInput is set to Blue when AmountInput
		//loses focus. Also, if AmountInput loses focus and no text is entered, the
		//text "Amount" is displayed.
		private void TextInputAmount_LostFocus(object sender, RoutedEventArgs e) {
			if (AmountInput.Text == String.Empty) {
				AmountInput.Text = "Amount";
				AmountInput.Foreground = new SolidColorBrush(ApplicationConstants.placeholderColor);
			}
		}
		
		private void CreatePayMe(object sender, RoutedEventArgs e) {		
			if (validateNewPayMe()) {
				string title = this.TitleInput.Text;
				string amount = this.AmountInput.Text;

				App.PayMeList.AddPayMe(new PayMeItemModel(title, 
														contactList.ParticipantList, 
														Convert.ToDouble(amount.Replace(".",","))), 
									ApplicationConstants.insertTrue);
				App.PayMeList.SaveToDisk();
				
				EmailComposeTask emailComposer = new EmailComposeTask();
				emailComposer.Subject = "Subject de prueba";
				emailComposer.Body = "Body de prueba";
				emailComposer.To = contactList.ParticipantList[0].Email + ";" + contactList.ParticipantList[0].Email;
				emailComposer.Show();

				NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.RelativeOrAbsolute));
			}
		}
		
		private bool validateNewPayMe() {
			bool result = true;
			
			if (ApplicationConstants.placeholderColor.Equals((TitleInput.Foreground as SolidColorBrush).Color)) {
				MessageBox.Show("Title is required");
				return false;
            } else if (ApplicationConstants.placeholderColor.Equals((AmountInput.Foreground as SolidColorBrush).Color)) {
                MessageBox.Show("Amount is required");
                return false;
            } else if (contactList.ParticipantList.Count < 2) {
                MessageBox.Show("At least two participants are required");
                return false;
            }
			
			return result;
		}

		private void TextInputAmount_KeyDown(object sender, KeyEventArgs e) {
			string amount = this.AmountInput.Text;
			
			if (Regex.IsMatch(amount, "^\\d*\\.\\d{2}$") || 
				(Regex.IsMatch(amount, "^$") && e.PlatformKeyCode.ToString().Equals("190")) ||
				(amount.Contains(".") && e.PlatformKeyCode.ToString().Equals("190")) ||
				(amount.Equals("0") && !e.PlatformKeyCode.ToString().Equals("190"))) {
				e.Handled = true;
			}
		}
    }
}
