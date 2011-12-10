using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Microsoft.Win32;

using AddressBook;

namespace AddressBookClient
{
    /// <summary>
    /// Interaction logic for ContactViewAndEdit.xaml
    /// </summary>
    public partial class ContactViewAndEdit : UserControl
    {
        public event EventHandler Saved;

        public ContactViewAndEdit()
        {
            InitializeComponent();
        }

        public void SetContact(Contact contact)
        {
            DataContext = new ContactViewModel(contact);
        }
        
        private void IMServiceButton_Click(object sender, RoutedEventArgs args)
        {
            Button source = args.Source as Button;
            IMType type = (IMType)Enum.Parse(typeof(IMType), source.Tag.ToString());
            IMAccount acc = new IMAccount { Type = type };
            ((ContactViewModel)DataContext).IMAccounts.Add(acc);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs args)
        {
            ((ContactViewModel)DataContext).FillBack();
            OnSaved();
        }

        private void OnSaved()
        {
            if (Saved != null) Saved(this, EventArgs.Empty);
        }
    }
}
