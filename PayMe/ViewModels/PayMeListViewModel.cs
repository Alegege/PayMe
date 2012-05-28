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
using System.Xml;
using System.IO.IsolatedStorage;
using System.Xml.Serialization;
using System.IO;

namespace PayMe {

    public class PayMeListViewModel : INotifyPropertyChanged
    {

        private ObservableCollection<PayMeItemModel> _PayMes = new ObservableCollection<PayMeItemModel>();

        // Declare the PropertyChanged event
        public event PropertyChangedEventHandler PropertyChanged;
		
        public PayMeListViewModel() {
            this.PayMes = new ObservableCollection<PayMeItemModel>();
        }

        public ObservableCollection<PayMeItemModel> PayMes
        {
            get
            {
                return _PayMes;
            }
            set
            {
                if (value != _PayMes)
                {
                    _PayMes = value;
                    NotifyPropertyChanged("PayMes");
                }
            }
        }

        public bool IsDataLoaded {
            get;
            private set;
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

        public void AddPayMe(PayMeItemModel payMe, bool insert) {
            if (PayMes == null) {
                PayMes = new ObservableCollection<PayMeItemModel>();
            }

			if (insert) {
				PayMes.Insert(0, payMe);
			} else {
				PayMes.Add(payMe);
			}
            
        }
		
		public void RemovePayMe(DateTime creationDate) {
			if (creationDate != null) {
				for (int i = 0; i < PayMes.Count; i++) {
                    if (DateTime.Compare(creationDate,PayMes[i].CreationDate) == 0)
                    {
						PayMes.RemoveAt(i);
					}
				}
			}
		}

        public PayMeItemModel GetPayMe(long creationDateTicks)
        {
            if (creationDateTicks > 0)
            {
                foreach (PayMeItemModel payMe in PayMes)

                {
                    if (creationDateTicks == payMe.CreationDate.Ticks)
                    {
                        return payMe;
                    }
                }
            }

            return new PayMeItemModel();
        }

        /// <summary>
        /// Crea y agrega algunos objetos ItemViewModel a la colección Items.
        /// </summary>
        public void LoadData() {
            try {
                using (IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication()) {
                    using (IsolatedStorageFileStream stream = myIsolatedStorage.OpenFile("PayMes.xml", FileMode.Open, FileAccess.Read)) {
                        XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<PayMeItemModel>));
                        ObservableCollection<PayMeItemModel> data = (ObservableCollection<PayMeItemModel>)serializer.Deserialize(stream);
                        this.PayMes.Clear();

                        foreach (PayMeItemModel p in data) {
                            this.AddPayMe(p, ApplicationConstants.insertFalse);
                        }

                        this.IsDataLoaded = true;
                    }
                }
            }
            catch (Exception e) {
                System.Diagnostics.Debug.WriteLine("Exception while loading stops from IsolatedStorage.");
            }
        }

        public void SaveToDisk() {
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.Indent = true;

            using (IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication()) {
                using (IsolatedStorageFileStream stream = myIsolatedStorage.OpenFile("PayMes.xml", FileMode.Create, FileAccess.Write)) {
                    XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<PayMeItemModel>));
                    using (XmlWriter xmlWriter = XmlWriter.Create(stream, xmlWriterSettings)) {
                        serializer.Serialize(xmlWriter, this.PayMes);
                    }
                }
            }            
        }
    }
}