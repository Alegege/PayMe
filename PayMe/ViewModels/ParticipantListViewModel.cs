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
using PayMe.Models;

namespace PayMe {
    public class ParticipantListViewModel {

        public ObservableCollection<ParticipantItemModel> ParticipantList { get; private set; }

        public ParticipantListViewModel() {
            this.ParticipantList = new ObservableCollection<ParticipantItemModel>();
        }

        public void addParticipant(EmailResult contact) {
            ParticipantList.Add(new ParticipantItemModel(contact.Email));
        }

        public bool existsParticipant(EmailResult newContact)
        {
            foreach (ParticipantItemModel participant in ParticipantList)
            {
                if (participant.Email.Equals(newContact.Email)) {
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
                        this.ParticipantList.Clear();

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
