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
using Microsoft.Phone.UserData;
using System.Linq;


namespace PayMe {
    public partial class NewPayMePage : PhoneApplicationPage {

        private ParticipantListViewModel _ParticipantList;

		private EmailAddressChooserTask emailTask;
		
        public NewPayMePage() {
            InitializeComponent();

            _ParticipantList = new ParticipantListViewModel();
            this.Participants.DataContext = _ParticipantList;

			emailTask = new EmailAddressChooserTask();
    		emailTask.Completed += new EventHandler<EmailResult>(emailTask_Completed);
        }

        public ParticipantListViewModel ParticipantList
        {
            get
            {
                // Retrasar la creación del modelo de vista hasta que sea necesario
                if (_ParticipantList == null)
                    _ParticipantList = new ParticipantListViewModel();

                return _ParticipantList;
            }
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
                if (!_ParticipantList.ExistsParticipant(contact)) {
                    
                    Contacts contacts = new Contacts();
                    contacts.SearchCompleted += new EventHandler<ContactsSearchEventArgs>(contacts_SearchCompleted);
                    contacts.SearchAsync(contact.DisplayName, FilterKind.DisplayName, contact.Email);
				}				
			}
		}

        void contacts_SearchCompleted(object sender, ContactsSearchEventArgs e)
        {
            string emailSelected = e.State.ToString();
            Contact contact = null;

            foreach (var result in e.Results)
            {
                foreach (ContactEmailAddress contactEmail in result.EmailAddresses)
                {
                    if (emailSelected.Equals(contactEmail.EmailAddress) && e.Filter.Equals(result.DisplayName))
                    {
                        contact = result;
                        break;
                    }
                }
            }

            if (contact != null)
            {
                _ParticipantList.AddParticipant(contact, emailSelected);
            }
            else
            {
                try
                {
                    IEnumerable<Contact> contactsLinq =
                        from Contact con in e.Results
                        from Account a in con.Accounts
                        where con.DisplayName.Equals(e.Filter) && a.Kind == StorageKind.Facebook
                        select con;

                    if (contactsLinq.Count() > 0)
                    {
                        _ParticipantList.AddParticipant(contactsLinq.First(), emailSelected);
                    }
                }
                catch (System.Exception)
                {
                    //No results
                }
            }

            this.Scroll.ScrollToVerticalOffset(this.ContentPanel.ActualHeight + 100.0);
        }

        private void MenuItem_DeleteParticipantClick(object sender, RoutedEventArgs e)
        {
            MenuItem itm = sender as MenuItem;

            _ParticipantList.RemoveParticipant((string)itm.CommandParameter);
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
														_ParticipantList.Participants, 
														Convert.ToDouble(amount.Replace(".",","))), 
									ApplicationConstants.insertTrue);
				App.PayMeList.SaveToDisk();
				
				EmailComposeTask emailComposer = new EmailComposeTask();
				emailComposer.Subject = "Subject de prueba";
				emailComposer.Body = "Body de prueba";
				emailComposer.To = _ParticipantList.Participants[0].Email + ";" + _ParticipantList.Participants[0].Email;
				emailComposer.Show();

				NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.RelativeOrAbsolute));
			}
		}
		
		private bool validateNewPayMe() {
			bool result = true;
			
			if (ApplicationConstants.placeholderColor.Equals((TitleInput.Foreground as SolidColorBrush).Color)) {
				MessageBox.Show("Title is required");
                this.Scroll.ScrollToVerticalOffset(0.0);
				return false;
            } else if (ApplicationConstants.placeholderColor.Equals((AmountInput.Foreground as SolidColorBrush).Color)) {
                MessageBox.Show("Amount is required");
                this.Scroll.ScrollToVerticalOffset(0.0);
                return false;
            } else if (_ParticipantList.Participants.Count < 2) {
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
