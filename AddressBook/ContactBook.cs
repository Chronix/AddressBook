using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace AddressBook
{
    [Serializable]
    public class ContactBook
    {
        public RefreshableCollection<Contact> Contacts { get; set; }

        [XmlIgnore]
        public bool ChangedSinceSave { get; set; }

        [XmlIgnore]
        public string SavePath { get; set; }

        public ContactBook()
        {
            Contacts = new RefreshableCollection<Contact>();
        }

        public void Refresh()
        {
            Contacts.Refresh();
        }

        public void Add(Contact ct)
        {
            Contacts.Add(ct);
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
