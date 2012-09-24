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

namespace PayMe
{
    public class ContactPictureItemModel<T, Q> : IComparable
    {
        private T _Key;
        private Q _Value;

        public ContactPictureItemModel(T key, Q value)
        {
            this._Key = key;
            this._Value = value;
        }

        public T Key
        {
            get
            {
                return _Key;
            }
            set
            {
                _Key = value;
            }
        }

        public Q Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = value;
            }
        }

        public int CompareTo(object obj)
        {
            ContactPictureItemModel<string, byte[]> contactPicture = obj as ContactPictureItemModel<string, byte[]>;
            string key = _Key as string;

            return key.CompareTo(contactPicture._Key);
        }
    }
}
