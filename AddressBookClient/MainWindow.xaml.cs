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

using AddressBook;

namespace AddressBookClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ContactBook _currentBook = null;

        public MainWindow()
        {
            InitializeComponent();
            contactEditor.SetContact(new AddressBook.Contact());
        }

        private void MenuItem_Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void CommandBinding_New_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (CheckForSave() == null) return;
            else _currentBook = new ContactBook();
        }

        private void CommandBinding_Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (CheckForSave() == null) return;

            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.Filter = "Contact Book (*.xml)|*.xml";
            ofd.CheckFileExists = true;

            if (ofd.ShowDialog().Value)
            {
                _currentBook = ContactBook.Load(ofd.FileName);
            }
        }

        private void CommandBinding_Close_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (CheckForSave() == null) return;
            _currentBook = null;
        }

        private void CommandBinding_Close_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _currentBook != null;
        }

        private void CommandBinding_Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DoSave();
        }

        private void CommandBinding_Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _currentBook != null;
        }

        private void CommandBinding_SaveAs_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DoSave(true);
        }

        private void CommandBinding_SaveAs_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _currentBook != null;
        }

        private bool? CheckForSave()
        {
            if (_currentBook.ChangedSinceSave)
            {
                switch (MessageBox.Show("Save current contact book?", "Save?", MessageBoxButton.YesNoCancel, MessageBoxImage.Question))
                {
                    case MessageBoxResult.Yes: DoSave(); return true;
                    case MessageBoxResult.No: return false;
                    case MessageBoxResult.Cancel: return null;
                }
            }

            return true;
        }

        private void DoSave(bool forceDialog = false)
        {
            if (!string.IsNullOrWhiteSpace(_currentBook.SavePath) && !forceDialog)
            {
                _currentBook.Save();
                return;
            }

            Microsoft.Win32.SaveFileDialog sfd = new Microsoft.Win32.SaveFileDialog();
            sfd.Filter = "Contact Book (*.xml)|*.xml";

            if (sfd.ShowDialog(this).Value)
            {
                _currentBook.Save(sfd.FileName);
            }
        }
    }
}
