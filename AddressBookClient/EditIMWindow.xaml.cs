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
using System.Windows.Shapes;

using AddressBook;

namespace AddressBookClient
{
    /// <summary>
    /// Interaction logic for EditIMWindow.xaml
    /// </summary>
    public partial class EditIMWindow : Window
    {
        public RefreshableCollection<IMAccount> Accounts { get; private set; }

        public EditIMWindow()
        {
            Accounts = new RefreshableCollection<IMAccount>();
            InitializeComponent();
        }

        private void IMServiceButton_Click(object sender, RoutedEventArgs args)
        {
        }
    }
}
