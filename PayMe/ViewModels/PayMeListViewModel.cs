using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace PayMe {
	
    public class PayMeListViewModel  {
		
		public ObservableCollection<PayMeItemViewModel> PayMes { get; private set; }
		
        public PayMeListViewModel() {
            this.PayMes = new ObservableCollection<PayMeItemViewModel>();
        }

        public bool IsDataLoaded {
            get;
            private set;
        }

        /// <summary>
        /// Crea y agrega algunos objetos ItemViewModel a la colección Items.
        /// </summary>
        public void LoadData() {
            // Datos de ejemplo; reemplazar por datos reales
            this.PayMes.Add(new PayMeItemViewModel("Cumpleaños Sergi", 17, 78.12, 12, DateTime.Parse("20/04/2012")));
            this.PayMes.Add(new PayMeItemViewModel("Barbacoa casa Pau", 28, 125.66, 3, DateTime.Parse("13/02/2012")));
			this.PayMes.Add(new PayMeItemViewModel("Regalo Jordi", 22, 65.01, 21, DateTime.Parse("12/04/2012")));
			this.PayMes.Add(new PayMeItemViewModel("Karts despedida Antonio", 11, 98, 5, DateTime.Parse("23/04/2012")));

            this.IsDataLoaded = true;
        }
    }
}