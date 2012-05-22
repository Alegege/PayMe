using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Windows.Navigation;

namespace PayMe
{
    public partial class ViewPayMe : PhoneApplicationPage
    {
        public PayMeItemModel payMe = null;

        public ViewPayMe()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo (NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string creationDateTicks = "";
            if (NavigationContext.QueryString.TryGetValue("creationDateTicks", out creationDateTicks))
            {
                payMe = App.PayMeList.GetPayMe(Int64.Parse(creationDateTicks));
                loadViewInfo();
            }                
        }

        private void loadViewInfo()
        {
            if (payMe != null)
            {
                this.TitleTB.Text = payMe.Title;
                this.AmountTB.Text = payMe.TotalAmount.ToString();
                this.EmailListTB.Text = payMe.ContactEmails;
            }
        }
    }
}
