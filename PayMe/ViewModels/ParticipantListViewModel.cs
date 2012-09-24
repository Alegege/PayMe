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
using System.ComponentModel;
using System.Collections.Specialized;
using PayMes;

namespace PayMe {
    public class ParticipantListViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ParticipantItemModel> _Participants = new ObservableCollection<ParticipantItemModel>();

        // Declare the PropertyChanged event
        public event PropertyChangedEventHandler PropertyChanged;

        public ParticipantListViewModel() {
            this._Participants = new ObservableCollection<ParticipantItemModel>();
        }

        public ObservableCollection<ParticipantItemModel> Participants
        {
            get
            {
                return _Participants;
            }
            set
            {
                if (value != _Participants)
                {
                    _Participants = value;
                    NotifyPropertyChanged("Participants");
                }
            }
        }

        // NotifyPropertyChanged will raise the PropertyChanged event passing the
        // source property that is being updated.
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void AddParticipant(Contact contact, string email) {
            App.ContactPictures.UpdateContactPictures(contact, email);
            Participants.Add(new ParticipantItemModel(contact, email));
            NotifyPropertyChanged("Participants");
        }

        public void RemoveParticipant(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                for (int i = 0; i < _Participants.Count; i++)
                {
                    if (email.Equals(_Participants[i].Email))
                    {
                        _Participants.RemoveAt(i);
                    }
                }
            }
        }

        public bool ExistsParticipant(EmailResult newContact)
        {
            foreach (ParticipantItemModel participant in Participants)
            {
                if (participant.Email.Equals(newContact.Email)) {
                    return true;
                }
            }

            return false;
        }

        public int PaidParticipants()
        {
            int paidParticipants = 0;

            foreach (ParticipantItemModel participant in _Participants)
            {
                if (participant.Paid)
                {
                    paidParticipants++;
                }
            }
            
            return paidParticipants;
        }
    }
}
