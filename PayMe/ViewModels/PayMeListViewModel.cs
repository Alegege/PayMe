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

    public class PayMeListViewModel {
		
		public ObservableCollection<PayMeItemModel> PayMes { get; private set; }
		
        public PayMeListViewModel() {
            this.PayMes = new ObservableCollection<PayMeItemModel>();
        }

        public bool IsDataLoaded {
            get;
            private set;
        }

        public void AddPayMe(PayMeItemModel payMe) {
            if (PayMes == null) {
                PayMes = new ObservableCollection<PayMeItemModel>();
            }

            PayMes.Insert(0, payMe);
        }

        /// <summary>
        /// Crea y agrega algunos objetos ItemViewModel a la colección Items.
        /// </summary>
        public void LoadData() {
            try {
                using (IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication()) {
                    using (IsolatedStorageFileStream stream = myIsolatedStorage.OpenFile("PayMes.xml", FileMode.Open, FileAccess.Read)) {
                        XmlSerializer serializer = new XmlSerializer(typeof(List<PayMeItemModel>));
                        List<PayMeItemModel> data = (List<PayMeItemModel>)serializer.Deserialize(stream);
                        this.PayMes.Clear();

                        foreach (PayMeItemModel p in data) {
                            this.AddPayMe(p);
                        }

                        this.IsDataLoaded = true;
                    }
                }
            }
            catch {
                System.Diagnostics.Debug.WriteLine("Exception while loading stops from IsolatedStorage.");
            }
        }

        public void SaveToDisk() {
            if (PayMes.Count > 0) {
                XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
                xmlWriterSettings.Indent = true;

                using (IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication()) {
                    using (IsolatedStorageFileStream stream = myIsolatedStorage.OpenFile("PayMes.xml", FileMode.Create, FileAccess.Write)) {
                        XmlSerializer serializer = new XmlSerializer(typeof(List<PayMeItemModel>));
                        using (XmlWriter xmlWriter = XmlWriter.Create(stream, xmlWriterSettings)) {
                            serializer.Serialize(xmlWriter, new List<PayMeItemModel>(this.PayMes));
                        }
                    }
                }
            }
        }
    }
}