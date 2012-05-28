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
using System.Collections.ObjectModel;
using Microsoft.Phone.Tasks;
using PayMe.Models;
using System.Globalization;
using System.Threading;

namespace PayMe {
    public class PayMeItemModel : INotifyPropertyChanged
    {
        private string _Title;
        private ParticipantListViewModel _ParticipantList = new ParticipantListViewModel();
        private double _TotalAmount;
        private double _PartialAmount;
        private DateTime _CreationDate;

        // Declare the PropertyChanged event
        public event PropertyChangedEventHandler PropertyChanged;

		public PayMeItemModel() {
		}

        public PayMeItemModel(string title, ObservableCollection<ParticipantItemModel> participants, double totalAmount) 
        {
            this._Title = title;
            this._ParticipantList.Participants = participants;
            this._TotalAmount = totalAmount;
            this._PartialAmount = Math.Ceiling((totalAmount / participants.Count) * 100) / 100;

            this._CreationDate = DateTime.Now;
		}

        public PayMeItemModel(string title, ObservableCollection<ParticipantItemModel> participants, double totalAmount, DateTime creationDate)
            : this(title, participants, totalAmount) 
        {

                this._CreationDate = creationDate;
		}

        public string Title
        {
            get
            {
                return _Title;
            }
            set
            {
                if (value != _Title)
                {
                    _Title = value;
                    NotifyPropertyChanged("Title");
                }
            }
        }

        public ParticipantListViewModel ParticipantList
        {
            get
            {
                return _ParticipantList;
            }
            set
            {
                if (value != _ParticipantList)
                {
                    _ParticipantList = value;
                    NotifyPropertyChanged("ParticipantList");
                }
            }
        }

        public double TotalAmount
        {
            get
            {
                return _TotalAmount;
            }
            set
            {
                if (value != _TotalAmount)
                {
                    _TotalAmount = value;
                    NotifyPropertyChanged("TotalAmount");
                }
            }
        }

        public double PartialAmount
        {
            get
            {
                return _PartialAmount;
            }
            set
            {
                if (value != _PartialAmount)
                {
                    _PartialAmount = value;
                    NotifyPropertyChanged("PartialAmount");
                }
            }
        }

        public int PaidParticipants
        {
            get
            {
                return _ParticipantList.PaidParticipants();
            }
        }

        public DateTime CreationDate
        {
            get
            {
                return _CreationDate;
            }
            set
            {
                if (value != _CreationDate)
                {
                    _CreationDate = value;
                    NotifyPropertyChanged("CreationDate");
                }
            }
        }

        private List<string> getEmailList()
        {
            List<string> emailList = new List<string>();

            foreach (ParticipantItemModel participant in ParticipantList.Participants)
            {
                emailList.Add(participant.Email);
            }

            return emailList;
        }

        public string ContactEmails
        {
            get
            {
                string result = null;

                foreach (ParticipantItemModel participant in ParticipantList.Participants)
                {
                    result += participant.Email + "\n";
                }

                return result;
            }
        }
		
		public string PaidAmount
		{
			get
			{
				return (this.PartialAmount * this.PaidParticipants).ToString("c");
			}
		}

        public string PaidAmountTB
        {
            get
            {
                return "PAID (" + PaidAmount + ")";
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
		
		public string PaidParticipantsValue {
            get {
                
                return string.Format("Paid: {0} of {1}  ({2} of {3})", this.PaidParticipants, this.ParticipantList.Participants.Count, this.PaidAmount, this.TotalAmount.ToString("c"));
            }
        }
		
		public string CreationDateValue {
            get {
                return string.Format("{0} {1}", "Created:", String.Format("{0:d/M/yyyy}", this.CreationDate));
            }
        }
		
		public long CreationDateTicks {
            get {
                return CreationDate.Ticks;
            }
        }
		
		public ParticipantItemModel getParticipant(string email) {
            foreach (ParticipantItemModel participant in ParticipantList.Participants)
            {
				if (email.Equals(participant.Email)) {
					return participant;
				}
			}
			
			return new ParticipantItemModel();
		}
	}
}