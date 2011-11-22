﻿using System;
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

using AddressBook;

namespace AddressBookClient
{
    /// <summary>
    /// Interaction logic for ContactViewAndEdit.xaml
    /// </summary>
    public partial class ContactViewAndEdit : UserControl
    {
        public ContactViewAndEdit()
        {
            InitializeComponent();
        }

        public void SetContact(Contact contact)
        {
            DataContext = new ContactViewModel(contact);
        }
    }
}