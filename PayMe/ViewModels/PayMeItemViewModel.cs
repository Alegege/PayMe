using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace PayMe
{
	public class PayMeItemViewModel : INotifyPropertyChanged 
	{
		public PayMeItemViewModel()
		{
			// A partir de este punto se requiere la inserción de código para la creación del objeto.
		}
		
		private string _title;
		
		private string _participants;
		
		private string _paidParticipants;
		
		private string _creationDate;
		
		public string Title 
        {
            get 
            {
                return _title;
            }
            set 
            {
                _title = value;
                NotifyPropertyChanged("Title");
            }
        }
		
		public string Participants 
        {
            get 
            {
                return _participants;
            }
            set 
            {
                _participants = value;
                NotifyPropertyChanged("Participants");
            }
        }
		
		public string PaidParticipants 
        {
            get 
            {
                return _paidParticipants;
            }
            set 
            {
                _paidParticipants = value;
                NotifyPropertyChanged("PaidParticipants");
            }
        }
		
		public string CreationDate 
        {
            get 
            {
                return _creationDate;
            }
            set 
            {
                _creationDate = value;
                NotifyPropertyChanged("CreationDate");
            }
        }
		
		public string PaidParticipantsValue 
        {
            get 
            {
                return string.Format("{0} {1} {2} {3}", "Paid:", this._paidParticipants, "of", this._participants);
            }
            set 
            {
                _creationDate = value;
                NotifyPropertyChanged("CreationDate");
            }
        }
		
		public string CreationDateValue 
        {
            get 
            {
                return string.Format("{0} {1}", "Created:", this._creationDate);
            }
            set 
            {
                _creationDate = value;
                NotifyPropertyChanged("CreationDate");
            }
        }
		
		public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName) 
        {
            if (null != PropertyChanged) 
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
	}
}