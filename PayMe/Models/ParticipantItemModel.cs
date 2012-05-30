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
using System.ComponentModel;
using System.Collections.Specialized;
using System.Collections;
using Microsoft.Phone.UserData;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;
using System.IO;

namespace PayMe.Models
{
    public class ParticipantItemModel : INotifyPropertyChanged
    {
        private string _Email;
        private string _DisplayName;
        private bool _Paid;

        public ParticipantItemModel() {
        }
		
		public ParticipantItemModel(string email)
        {
            this.Email = email;
            this.Paid = false;
        }

        public ParticipantItemModel(Contact contact, string email)
        {
            this._Email = email;
            this._DisplayName = contact.DisplayName;

            this._Paid = false;
        }

        public byte[] GetByteArrayFromImage(BitmapImage image)
        {
            WriteableBitmap bmp = new WriteableBitmap(image);

            using (MemoryStream stream = new MemoryStream())
            {
                bmp.SaveJpeg(stream, bmp.PixelWidth, bmp.PixelHeight, 0, 100);
                return stream.ToArray();
            }
        }


        public BitmapImage GetImageFromByteArray(byte[] byteArray)
        {
            using (MemoryStream stream = new MemoryStream(byteArray))
            {
                BitmapImage bmp = new BitmapImage();
                bmp.SetSource(stream);

                return bmp;
            }
        }

        public BitmapImage GetSourceImageFromContactPicture(System.IO.Stream stream)
        {
            BitmapImage imgSrc = new BitmapImage();

            if (stream != null)
            {
                imgSrc.SetSource(stream);
            }

            return imgSrc;
        }

        public string Email
        {
            get
            {
                return _Email;
            }
            set
            {
                if (value != _Email)
                {
                    _Email = value;
                    NotifyPropertyChanged("Email");
                }
            }
        }

        public string DisplayName
        {
            get
            {
                return _DisplayName;
            }
            set
            {
                if (value != _DisplayName)
                {
                    _DisplayName = value;
                    NotifyPropertyChanged("DisplayName");
                }
            }
        }

        public bool Paid
        {
            get
            {
                return _Paid;
            }
            set
            {
                if (value != _Paid)
                {
                    _Paid = value;
                    NotifyPropertyChanged("Paid");
                }
            }
        }
		
		// Declare the PropertyChanged event
		public event PropertyChangedEventHandler PropertyChanged;
	
		// NotifyPropertyChanged will raise the PropertyChanged event passing the
		// source property that is being updated.
		public void NotifyPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
        
    }
}
