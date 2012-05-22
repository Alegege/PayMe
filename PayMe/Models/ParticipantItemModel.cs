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

namespace PayMe.Models
{
    public class ParticipantItemModel
    {
        public string Email { get; set; }
        public bool Paid { get; set; }

        public ParticipantItemModel() {
        }

        public ParticipantItemModel(string email)
        {
            this.Email = email;
            this.Paid = false;
        }

    }
}
