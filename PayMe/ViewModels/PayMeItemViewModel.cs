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

namespace PayMe {
	public class PayMeItemViewModel {
		
		private string title;
		private int participants;
		private double totalAmount;
		private double partialAmount;
		private int paidParticipants;
		private DateTime creationDate;
		
		public PayMeItemViewModel() {
		}
		
		public PayMeItemViewModel(string title, int participants, double totalAmount) {
			this.title = title;
			this.participants = participants;
			this.totalAmount = totalAmount;
			
			if (participants <= 1) {
				this.partialAmount = totalAmount;
			} else {
                this.partialAmount = Math.Round(totalAmount / participants, 2);
			}
			
			this.paidParticipants = 0;
			this.creationDate = DateTime.Now;
		}
		
		public PayMeItemViewModel(string title, int participants, double totalAmount, int paidParticipants, DateTime creationDate) 
			: this (title, participants, totalAmount) {
				
			this.paidParticipants = paidParticipants;
			this.creationDate = creationDate;
		}
		
		public string Title {
            get {
                return title;
            }
            set {
                title = value;
                NotifyPropertyChanged("Title");
            }
        }
		
		public int Participants {
            get {
                return participants;
            }
            set {
                participants = value;
                NotifyPropertyChanged("Participants");
            }
        }
		
		public int PaidParticipants {
            get {
                return paidParticipants;
            }
            set {
                paidParticipants = value;
                NotifyPropertyChanged("PaidParticipants");
            }
        }
		
		public DateTime CreationDate {
            get {
                return creationDate;
            }
            set {
                creationDate = value;
                NotifyPropertyChanged("CreationDate");
            }
        }
		
		public double TotalAmount {
            get {
                return totalAmount;
            }
            set {
                totalAmount = value;
                NotifyPropertyChanged("TotalAmount");
            }
        }
		
		public double PartialAmount {
            get {
                return partialAmount;
            }
            set {
                partialAmount = value;
                NotifyPropertyChanged("PartialAmount");
            }
        }
		
		public string PaidParticipantsValue {
            get {
                return string.Format("Paid: {0} of {1}  ({2} € of {3} €)", this.paidParticipants, this.participants, this.partialAmount * this.paidParticipants, this.totalAmount);
            }
        }
		
		public string CreationDateValue {
            get {
                return string.Format("{0} {1}", "Created:", String.Format("{0:d/M/yyyy}", this.creationDate));
            }
        }
		
		public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName) {
            if (null != PropertyChanged) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
	}
}