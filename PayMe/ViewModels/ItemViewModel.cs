using System;
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

namespace PayMe
{
    public class ItemViewModel : INotifyPropertyChanged 
    {
        private string _lineOne;
        /// <summary>
        /// Propiedad ViewModel de ejemplo; esta propiedad se usa en la vista para mostrar su valor por medio de un enlace.
        /// </summary>
        /// <returns></returns>
        public string LineOne 
        {
            get 
            {
                return _lineOne;
            }
            set 
            {
                _lineOne = value;
                NotifyPropertyChanged("LineOne");
            }
        }
        
        private string _lineTwo;
        /// <summary>
        /// Propiedad ViewModel de ejemplo; esta propiedad se usa en la vista para mostrar su valor por medio de un enlace.
        /// </summary>
        /// <returns></returns>
        public string LineTwo
        {
            get
            {
                return _lineTwo;
            }
            set
            {
                _lineTwo = value;
                NotifyPropertyChanged("LineTwo");
            }
        }

        private string _lineThree;
        /// <summary>
        /// Propiedad ViewModel de ejemplo; esta propiedad se usa en la vista para mostrar su valor por medio de un enlace.
        /// </summary>
        /// <returns></returns>
        public string LineThree
        {
            get
            {
                return _lineThree;
            }
            set
            {
                _lineThree = value;
                NotifyPropertyChanged("LineThree");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName) 
        {
            if (null != PropertyChanged) 
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}