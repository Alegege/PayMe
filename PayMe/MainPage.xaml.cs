﻿using System;
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
using Microsoft.Phone.Shell;

namespace PayMe
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();			
            this.payMeList.DataContext = App.PayMeList;
            this.Loaded +=new RoutedEventHandler(MainPage_Loaded);		
        }
		
        // Cargar datos para los elementos de ViewModel
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.PayMeList.IsDataLoaded)
            {
                App.PayMeList.LoadData();
            }
        }
		
		public void PayMe_Tap(object sender, System.Windows.Input.GestureEventArgs e) {
			StackPanel stack = sender as StackPanel;
            TextBlock CreationDateTB = stack.FindName("CreationDateTicksTextBlock") as TextBlock;

            NavigationService.Navigate(new Uri("/ViewPayMe.xaml?creationDateTicks=" + CreationDateTB.Text, UriKind.RelativeOrAbsolute));
		}
		
		private void MenuItem_DeletePayMeClick(object sender, RoutedEventArgs e) {
            MenuItem itm = sender as MenuItem;

            App.PayMeList.RemovePayMe((DateTime)itm.CommandParameter);
        }
    }
}