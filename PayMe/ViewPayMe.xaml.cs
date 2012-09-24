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
using System.ComponentModel;

namespace PayMe
{
    public partial class ViewPayMe : PhoneApplicationPage
    {

        private PayMeItemModel _SelectedPayMe;

        public PayMeItemModel SelectedPayMe
        {
            get
            {
                // Retrasar la creación del modelo de vista hasta que sea necesario
                if (_SelectedPayMe == null)
                    _SelectedPayMe = new PayMeItemModel();

                return _SelectedPayMe;
            }
        }

        public ViewPayMe()
        {
            InitializeComponent();

            this.SelectedPayMe.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(usrControl_PropertyChanged);

            this.PaidAmountTB.DataContext = SelectedPayMe.PaidAmountTB;
            this.ParticipantList.DataContext = SelectedPayMe;
        }

        void usrControl_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.PropertyName))
            {
                if (e.PropertyName.Equals("Paid"))
                {
                    this.PaidAmountTB.Text = _SelectedPayMe.PaidAmountTB;
                }
            }
        }
		
        protected override void OnNavigatedTo (NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string creationDateTicks = "";
            if (NavigationContext.QueryString.TryGetValue("creationDateTicks", out creationDateTicks))
            {
                _SelectedPayMe = App.PayMeList.GetPayMe(Int64.Parse(creationDateTicks));
                loadViewInfo();
            }                
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            App.PayMeList.SaveToDisk();
        }

        private void loadViewInfo()
        {
            if (_SelectedPayMe != null)
            {
                this.TitleTB.Text = _SelectedPayMe.Title;
                this.AmountTB.Text = _SelectedPayMe.TotalAmount.ToString("c");
				this.AmountEachTB.Text = "   (" + _SelectedPayMe.PartialAmount.ToString("c") + " each)";
                this.PaidAmountTB.Text = _SelectedPayMe.PaidAmountTB;
                this.ParticipantList.DataContext = _SelectedPayMe.ParticipantList;
            }
        }

        private void Checkbox_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            this.PaidAmountTB.Text = _SelectedPayMe.PaidAmountTB;
        }

        
    }
}
