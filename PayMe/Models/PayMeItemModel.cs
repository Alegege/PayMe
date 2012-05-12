﻿using System;
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
	public class PayMeItemModel {
		
		private string title;
		private int participants;
		private double totalAmount;
		private double partialAmount;
		private int paidParticipants;
		private DateTime creationDate;
		
		public PayMeItemModel() {
		}
		
		public PayMeItemModel(string title, int participants, double totalAmount) {
			this.title = title;
			this.participants = participants;
			this.totalAmount = totalAmount;
			this.partialAmount = Math.Round(totalAmount / participants, 2);
			this.paidParticipants = 0;
			this.creationDate = DateTime.Now;
		}
		
		public PayMeItemModel(string title, int participants, double totalAmount, int paidParticipants, DateTime creationDate) 
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
            }
        }
		
		public int Participants {
            get {
                return participants;
            }
            set {
                participants = value;
            }
        }
		
		public int PaidParticipants {
            get {
                return paidParticipants;
            }
            set {
                paidParticipants = value;
            }
        }
		
		public DateTime CreationDate {
            get {
                return creationDate;
            }
            set {
                creationDate = value;
            }
        }
		
		public double TotalAmount {
            get {
                return totalAmount;
            }
            set {
                totalAmount = value;
            }
        }
		
		public double PartialAmount {
            get {
                return partialAmount;
            }
            set {
                partialAmount = value;
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
	}
}