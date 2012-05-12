using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using Microsoft.Phone.Tasks;
using System.IO.IsolatedStorage;
using System.IO;
using Microsoft.Phone.UserData;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace PayMe {
    public class ContactListViewModel {

        public ObservableCollection<EmailResult> Contacts { get; private set; }

        public ContactListViewModel() {
            this.Contacts = new ObservableCollection<EmailResult>();
        }

        public void addContact(EmailResult contact) {
            Contacts.Add(contact);
        }

        public bool existsContact(EmailResult newContact) {
            foreach (EmailResult contact in Contacts) {
                if (contact.Email.Equals(newContact.Email)) {
                    return true;
                }
            }

            return false;
        }

        public void LoadData() {
            try
            {
                using (IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    using (IsolatedStorageFileStream stream = myIsolatedStorage.OpenFile("Parades.xml", FileMode.Open))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(List<Contacts>));
                        List<Contacts> data = (List<Contacts>)serializer.Deserialize(stream);
                        this.Contacts.Clear();

                    }
                }
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("Exception while loading stops from IsolatedStorage.");
            }
        }
    }
}
