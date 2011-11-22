using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

using AddressBook;

namespace AddressBookClient
{
    public class ContactViewModelItem
    {
        public string PropertyName { get; set; }
        public string DisplayName { get; set; }
        public string Value { get; set; }
    }

    public class ContactViewModel
    {
        private static readonly PropertyInfo[] _contactProps;
        private Contact _contact;

        public ObservableCollection<ContactViewModelItem> Items { get; private set; }

        public ContactViewModel(Contact contact)
        {
            Items = new ObservableCollection<ContactViewModelItem>();
            _contact = contact;

            foreach (PropertyInfo prop in _contactProps)
            {
                ContactViewModelItem item = new ContactViewModelItem
                {
                    PropertyName = prop.Name,
                    DisplayName = prop.Name
                };

                object val = prop.GetValue(_contact, null) ?? "";
                item.Value = val.ToString();
                Items.Add(item);
            }
        }

        static ContactViewModel()
        {
            _contactProps = typeof(Contact).GetProperties().Where(prop => prop.PropertyType == typeof(string)).ToArray();
        }

        public void FillBack()
        {
            foreach (PropertyInfo prop in _contactProps)
            {
                var item = Items.Single(cvmi => cvmi.PropertyName == prop.Name);
                prop.SetValue(_contact, item.Value, null);
            }
        }
    }
}
