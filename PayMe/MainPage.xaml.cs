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

            createApplicationBar();			
        }

        private void createApplicationBar()
        {
            //Create the ApplicationBar
            ApplicationBar = new ApplicationBar();
            ApplicationBar.IsVisible = true;
            ApplicationBar.IsMenuEnabled = true;
            //Create the icon buttons and setting its properties
            ApplicationBarIconButton newButton = new ApplicationBarIconButton(new Uri
            ("/icons/appbar.add.rest.png", UriKind.Relative));
            newButton.Text = "new";
            newButton.Click += new EventHandler(newButton_Click);
            ApplicationBarIconButton settingsButton = new ApplicationBarIconButton(new Uri
            ("/icons/appbar.feature.settings.rest.png", UriKind.Relative));
            settingsButton.Text = "settings";
            settingsButton.Click += new EventHandler(settingsButton_Click);
            //Add the icon buttons to the Application Bar
            ApplicationBar.Buttons.Add(newButton);
            ApplicationBar.Buttons.Add(settingsButton);
            //Create menu items
            ApplicationBarMenuItem settingsMenuItem = new ApplicationBarMenuItem
            ("about");
            settingsMenuItem.Click += new EventHandler(aboutMenuItem_Click);
            //Add the menu items to the Application Bar
            ApplicationBar.MenuItems.Add(settingsMenuItem);
        }

        // Cargar datos para los elementos de ViewModel
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.PayMeList.IsDataLoaded)
            {
                App.PayMeList.LoadData();
            }
        }

        private void newButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/NewPayMePage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Button 2 works!");
            //Do work for your application here.
        }

        private void aboutMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Button 3 works!");
            //Do work for your application here.
        }
    }
}