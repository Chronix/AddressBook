using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace AddressBook
{
    [Serializable]
    public class ContactBook : ICollection<Contact>
    {
        private RefreshableCollection<Contact> _contacts;

        public bool ChangedSinceSave { get; set; }
        public string SavePath { get; set; }

        #region ICOLLECTION MEMBERS
        public ContactBook()
        {
            _contacts = new RefreshableCollection<Contact>();
            ChangedSinceSave = true;
        }

        public void Add(Contact item)
        {
            _contacts.Add(item);
            ChangedSinceSave = true;
        }

        public void Clear()
        {
            _contacts.Clear();
            ChangedSinceSave = true;
        }

        public bool Contains(Contact item)
        {
            return _contacts.Contains(item);
        }

        public void CopyTo(Contact[] array, int arrayIndex)
        {
            _contacts.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _contacts.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(Contact item)
        {
            bool removed = _contacts.Remove(item);
            ChangedSinceSave = removed;
            return removed;
        }

        public IEnumerator<Contact> GetEnumerator()
        {
            return _contacts.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _contacts.GetEnumerator();
        }
        #endregion

        public void Refresh()
        {
            _contacts.Refresh();
        }

        public void Save(string path)
        {
            XmlSerializer xs = new XmlSerializer(GetType());

            using (XmlTextWriter wr = new XmlTextWriter(path, Encoding.UTF8))
            {
                wr.Formatting = Formatting.Indented;
                xs.Serialize(wr, this);
            }

            ChangedSinceSave = false;
            SavePath = path;
        }

        public void Save()
        {
            if (string.IsNullOrWhiteSpace(SavePath)) throw new ArgumentNullException("No known save path, please call Save(string path).");
            Save(SavePath);
        }

        public static ContactBook Load(string path)
        {
            ContactBook book;

            XmlSerializer xs = new XmlSerializer(typeof(ContactBook));

            using (XmlReader xr = new XmlTextReader(path))
            {
                book = xs.Deserialize(xr) as ContactBook;
            }

            book.ChangedSinceSave = false;

            return book;
        }
    }
}
