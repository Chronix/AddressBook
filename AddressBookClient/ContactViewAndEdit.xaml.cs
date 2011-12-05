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
        public Action OnSave { get; set; }

        public ContactViewAndEdit()
        {
            InitializeComponent();
        }

        public void SetContact(Contact contact)
        {
            DataContext = new ContactViewModel(contact);
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Pictures|*.jpg;*.jpeg;*.gif;*.png;*.bmp";
            ofd.CheckFileExists = true;
            ofd.Multiselect = false;
            bool? res = ofd.ShowDialog();
            
            if (res.HasValue && res.Value)
            {
                imagePathBox.Text = ofd.FileName;
            }
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
            if (OnSave != null) OnSave();
        }
    }
}
