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
using System.Collections.Generic;
using Microsoft.Phone.UserData;
using System.IO;
using System.Windows.Media.Imaging;
using System.IO.IsolatedStorage;
using System.Xml.Serialization;
using System.Collections.ObjectModel;
using System.Xml;

namespace PayMe
{
    public class ContactPicturesDictionaryModel
    {
        //private Dictionary<string, byte[]> _ContactPictures;

        private ObservableCollection<ContactPictureItemModel<string, byte[]>> _ContactPictures;


        //public Dictionary<string, byte[]> ContactPictures
        //{
        //    get
        //    {
        //        // Retrasar la creación del modelo de vista hasta que sea necesario
        //        if (_ContactPictures == null)
        //            _ContactPictures = new Dictionary<string, byte[]>();

        //        return _ContactPictures;
        //    }

        //    set
        //    {
        //        if (value != null)
        //        {
        //            _ContactPictures = value;
        //        }
        //    }
        //}

        public ObservableCollection<ContactPictureItemModel<string, byte[]>> ContactPictures
        {
            get
            {
                // Retrasar la creación del modelo de vista hasta que sea necesario
                if (_ContactPictures == null)
                    _ContactPictures = new ObservableCollection<ContactPictureItemModel<string, byte[]>>();

                return _ContactPictures;
            }

            set
            {
                if (value != null)
                {
                    _ContactPictures = value;
                }
            }
        }

        public void UpdateContactPictures(Contact contactToUpdate, string email)
        {
            ContactPictureItemModel<string, byte[]> contact = this.GetContactPictureItem(email);

            if (contact != null)
            {
                contact.Value = GetByteArrayFromImageStream(contactToUpdate.GetPicture());
            }
            else
            {
                //ContactPictures.Add(email, GetByteArrayFromImageStream(contact.GetPicture()));
                ContactPictures.Add(new ContactPictureItemModel<string, byte[]>(email, GetByteArrayFromImageStream(contactToUpdate.GetPicture())));
            }
        }

        public ContactPictureItemModel<string, byte[]> GetContactPictureItem(string email)
        {
            ContactPictureItemModel<string, byte[]> contactPictureResult = null;

            foreach (ContactPictureItemModel<string, byte[]> contact in ContactPictures)
            {
                if (email.Equals(contact.Key))
                {
                    contactPictureResult = contact;
                    break;
                }
            }

            return contactPictureResult;
        }

        public BitmapImage GetContactPicture(string email)
        {
            return this.GetImageFromByteArray(this.GetContactPictureItem(email).Value);
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

        public byte[] GetByteArrayFromImageStream(Stream imageStream)
        {
            BitmapImage imgSrc = new BitmapImage();

            if (imageStream != null)
            {
                imgSrc.SetSource(imageStream);

                WriteableBitmap bmp = new WriteableBitmap(imgSrc);

                using (MemoryStream stream = new MemoryStream())
                {
                    bmp.SaveJpeg(stream, bmp.PixelWidth, bmp.PixelHeight, 0, 100);
                    return stream.ToArray();
                }

            }

            return new byte[1];
        }

        public void LoadData()
        {
            try
            {
                using (IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    using (IsolatedStorageFileStream stream = myIsolatedStorage.OpenFile("ContactPictures.xml", FileMode.Open, FileAccess.Read))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<ContactPictureItemModel<string, byte[]>>));
                        ObservableCollection<ContactPictureItemModel<string, byte[]>> data = (ObservableCollection<ContactPictureItemModel<string, byte[]>>)serializer.Deserialize(stream);
                        this.ContactPictures.Clear();

                        foreach (ContactPictureItemModel<string, byte[]> p in data)
                        {
                            this.ContactPictures.Add(p);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception while loading stops from IsolatedStorage.");
            }
        }

        public void SaveToDisk()
        {
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.Indent = true;

            try
            {
                using (IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    using (IsolatedStorageFileStream stream = myIsolatedStorage.OpenFile("ContactPictures.xml", FileMode.Create, FileAccess.Write))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<ContactPictureItemModel<string, byte[]>>));
                        using (XmlWriter xmlWriter = XmlWriter.Create(stream, xmlWriterSettings))
                        {
                            serializer.Serialize(xmlWriter, this.ContactPictures);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception while loading stops from IsolatedStorage.");
            }
        }
    }
}
