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
using System.Collections.Generic;
using System.Linq;
using PayMe;

namespace PayMes
{
    public class ParticipantItemModel : INotifyPropertyChanged
    {
        private string _Email;
        private string _DisplayName;
        private bool _Paid;
        [XmlIgnore]
        private BitmapImage _ContactPicture;

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
            this._ContactPicture = GetSourceImageFromContactPicture(contact.GetPicture());
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

        [XmlIgnore]
        public BitmapImage ContactPictureSource
        {
            get
            {
                return App.ContactPictures.GetContactPicture(Email);
                //utilizar el método de aquí abajo, y que cuando termine el handler se eleve una notificación de propiedad modificada. La capturo en el ParticipantListViewModel y elevo
                //otra para capturarla en el PayMeItemModel. 
                //if (_ContactPicture != null)
                //{
                //    return _ContactPicture;
                //}
                //else
                //{
                //    return null;
                //}
                //else if (_PictureStream != null)
                //{
                //    BitmapImage imgSrc = new BitmapImage();
                //    imgSrc.SetSource(_PictureStream);

                //    return imgSrc;
                //    //return GetImageFromByteArray(App.ContactPictures.ContactPictures[Email]);
                //}
                //else
                //{
                //    Contacts contacts = new Contacts();
                //    contacts.SearchCompleted += new EventHandler<ContactsSearchEventArgs>(contacts_SearchCompleted);
                //    contacts.SearchAsync(DisplayName, FilterKind.DisplayName, Email);
                //    _IsSyncing = true;

                //    while (_IsSyncing)
                //    {
                //    }

                //    return _ContactPicture;
                //}
            }
        }

        public void GetPicture()
        {
            Contacts contacts = new Contacts();
            contacts.SearchCompleted += new EventHandler<ContactsSearchEventArgs>(contacts_SearchCompleted);
            contacts.SearchAsync(DisplayName, FilterKind.DisplayName, Email);
        }

        void contacts_SearchCompleted(object sender, ContactsSearchEventArgs e)
        {
            Contact contact = null;

            foreach (var result in e.Results)
            {
                foreach (ContactEmailAddress contactEmail in result.EmailAddresses)
                {
                    if (Email.Equals(contactEmail.EmailAddress))
                    {
                        contact = result;
                        break;
                    }
                }
            }

            if (contact != null)
            {
                this._ContactPicture = GetSourceImageFromContactPicture(contact.GetPicture());
            }
            else
            {
                try
                {
                    IEnumerable<Contact> contactsLinq =
                        from Contact con in e.Results
                        from Account a in con.Accounts
                        where con.DisplayName.Equals(e.Filter) && a.Kind == StorageKind.Facebook
                        select con;

                    if (contactsLinq.Count() > 0)
                    {
                        this._ContactPicture = GetSourceImageFromContactPicture(contactsLinq.First().GetPicture());
                    }
                }
                catch (System.Exception)
                {
                    //No results
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
