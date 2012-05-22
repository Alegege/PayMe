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

namespace PayMe {
	public class PayMeItemModel {
		
		public string Title { get; set; }
        //public int Participants { get; set; }
        public ObservableCollection<ParticipantItemModel> Participants { get; set; }
        public double TotalAmount { get; set; }
        public double PartialAmount { get; set; }
        public int PaidParticipants { get; set; }
        public DateTime CreationDate { get; set; }
		
		public PayMeItemModel() {
		}

        public PayMeItemModel(string title, ObservableCollection<ParticipantItemModel> participants, double totalAmount)
        {
            this.Title = title;
            this.Participants = participants;
            //this.Participants = participants.Count;
            this.TotalAmount = totalAmount;
            this.PartialAmount = Math.Round(totalAmount / participants.Count, 2);
            this.PaidParticipants = 0;
            this.CreationDate = DateTime.Now;
		}

        public PayMeItemModel(string title, ObservableCollection<ParticipantItemModel> participants, double totalAmount, int paidParticipants, DateTime creationDate)
            : this(title, participants, totalAmount) {

                this.PaidParticipants = paidParticipants;
                this.CreationDate = creationDate;
		}

        private List<string> getEmailList()
        {
            List<string> emailList = new List<string>();

            foreach (ParticipantItemModel participant in Participants)
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

                foreach (ParticipantItemModel participant in Participants)
                {
                    result += participant.Email + "\n";
                }

                return result;
            }
        }
		
		public string PaidParticipantsValue {
            get {
                return string.Format("Paid: {0} of {1}  ({2} € of {3} €)", this.PaidParticipants, this.Participants.Count, this.PartialAmount * this.PaidParticipants, this.TotalAmount);
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
	}
}