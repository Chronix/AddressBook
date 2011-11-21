using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AddressBook
{
    public class ContactBook : ICollection<Contact>
    {
        private RefreshableCollection<Contact> _contacts;

        #region ICOLLECTION MEMBERS
        public ContactBook()
        {
            _contacts = new RefreshableCollection<Contact>();
        }

        public void Add(Contact item)
        {
            _contacts.Add(item);
        }

        public void Clear()
        {
            _contacts.Clear();
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
            return _contacts.Remove(item);
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
    }
}
