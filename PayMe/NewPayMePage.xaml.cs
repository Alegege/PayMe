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

namespace PayMe {
    public partial class NewPayMePage : PhoneApplicationPage {

        public ContactListViewModel contactList { get; private set; }

		private EmailAddressChooserTask emailTask;
		
        public NewPayMePage() {
            InitializeComponent();

            contactList = new ContactListViewModel();
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
                if (!contactList.existsContact(contact)) {
                    contactList.addContact(contact);
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
            if (this.TitleInput.Text != String.Empty && this.TitleInput.Text != "Title"
                && this.AmountInput.Text != String.Empty && this.AmountInput.Text != "Amount"
                && contactList.Contacts.Count > 0) {
					App.PayMeList.PayMes.Insert(0, new PayMeItemModel(this.TitleInput.Text, contactList.Contacts.Count, Convert.ToDouble(this.AmountInput.Text.Replace(".",","))));
                    App.PayMeList.SaveToDisk();
					
					EmailComposeTask emailComposer = new EmailComposeTask();
					emailComposer.Subject = "Subject de prueba";
					emailComposer.Body = "Body de prueba";
					emailComposer.To = contactList.Contacts[0].Email + ";" + contactList.Contacts[0].Email;
					emailComposer.Show();				

					NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.RelativeOrAbsolute));
			}
		}		
    }
}
