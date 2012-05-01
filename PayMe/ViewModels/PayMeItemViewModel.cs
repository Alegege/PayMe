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
		private string title;
		
		private int participants;
		
		private int paidParticipants;
		
		private DateTime creationDate;
		
		public PayMeItemViewModel() {
		}
		
		public PayMeItemViewModel(string title, int participants)
		{
			this.title = title;
			this.participants = participants;
			this.paidParticipants = 0;
			this.creationDate = DateTime.Now;
		}
		
		public PayMeItemViewModel(string title, int participants, int paidParticipants, DateTime creationDate)
		{
			this.title = title;
			this.participants = participants;
			this.paidParticipants = paidParticipants;
			this.creationDate = creationDate;
		}
		
		
		
		public string Title 
        {
            get 
            {
                return title;
            }
            set 
            {
                title = value;
                NotifyPropertyChanged("Title");
            }
        }
		
		public int Participants 
        {
            get 
            {
                return participants;
            }
            set 
            {
                participants = value;
                NotifyPropertyChanged("Participants");
            }
        }
		
		public int PaidParticipants 
        {
            get 
            {
                return paidParticipants;
            }
            set 
            {
                paidParticipants = value;
                NotifyPropertyChanged("PaidParticipants");
            }
        }
		
		public DateTime CreationDate 
        {
            get 
            {
                return creationDate;
            }
            set 
            {
                creationDate = value;
                NotifyPropertyChanged("CreationDate");
            }
        }
		
		public string PaidParticipantsValue 
        {
            get 
            {
                return string.Format("{0} {1} {2} {3}", "Paid:", this.paidParticipants, "of", this.participants);
            }
        }
		
		public string CreationDateValue 
        {
            get 
            {
                return string.Format("{0} {1}", "Created:", this.creationDate);
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