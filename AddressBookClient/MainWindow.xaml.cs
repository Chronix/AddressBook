using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public static readonly DependencyProperty CurrentBookProperty = DependencyProperty.Register("CurrentBook", typeof(ContactBook), typeof(MainWindow));

        private ContactBook _currentBook = null;
        private ICollectionView _contactView = null;

        public ContactBook CurrentBook
        {
            get { return (ContactBook)GetValue(CurrentBookProperty); }
            set { SetValue(CurrentBookProperty, value); }
        }

        public MainWindow()
        {          
            InitializeComponent();
            contactEditor.Saved += new EventHandler(contactEditor_Saved);
        }

        private void MenuItem_Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        #region COMMAND HANDLERS
        private void CommandBinding_New_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (CheckForSave() == null) return;
            else SetCurrentBook(new ContactBook());
        }

        private void CommandBinding_Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (CheckForSave() == null) return;

            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.Filter = "Contact Book (*.xml)|*.xml";
            ofd.CheckFileExists = true;

            if (ofd.ShowDialog().Value)
            {
                SetCurrentBook(ContactBook.Load(ofd.FileName));
            }
        }

        private void CommandBinding_Close_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (CheckForSave() == null) return;
            SetCurrentBook(null);
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

        private void CommandBinding_SendEMail_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Contact ct = ((ListBoxItem)e.OriginalSource).Content as Contact;
            System.Diagnostics.Process.Start("mailto:" + ct.EMail);
        }

        private void CommandBinding_SendEMail_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            Contact ct = ((ListBoxItem)e.OriginalSource).Content as Contact;
            e.CanExecute = !string.IsNullOrWhiteSpace(ct.EMail);
        }

        private void CommandBinding_VOIPCall_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // nic
        }

        private void CommandBinding_VOIPCall_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            Contact ct = ((ListBoxItem)e.OriginalSource).Content as Contact;
            string param = e.Parameter.ToString();

            switch (param)
            {
                case "home": e.CanExecute = !string.IsNullOrWhiteSpace(ct.HomePhone); break;
                case "work": e.CanExecute = !string.IsNullOrWhiteSpace(ct.WorkPhone); break;
                case "cell": e.CanExecute = !string.IsNullOrWhiteSpace(ct.CellPhone); break;
            }
        }

        private void CommandBinding_ContactIM_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // nic :)
        }

        private void CommandBinding_ContactIM_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            Contact ct = ((ListBoxItem)e.OriginalSource).Content as Contact;
            string param = e.Parameter.ToString();
            IMType wantedType = (IMType)Enum.Parse(typeof(IMType), param);

            e.CanExecute = ct.HasIMContact(wantedType);
        }

        private void CommandBinding_AddContact_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Contact ct = new Contact();
            _currentBook.Add(ct);
            lstContacts.SelectedIndex = lstContacts.Items.Count - 1;
            if (contactEditor.Visibility == System.Windows.Visibility.Collapsed) contactEditor.Visibility = System.Windows.Visibility.Visible;
        }

        private void CommandBinding_AddContact_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _currentBook != null;
        }

        private void CommandBinding_RemoveContact_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _currentBook.Contacts.RemoveAt(lstContacts.SelectedIndex);
            if (_currentBook.Contacts.Count == 0) contactEditor.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void CommandBinding_RemoveContact_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _currentBook != null && _currentBook.Contacts.Count > 0 && lstContacts.SelectedIndex != -1;
        }
        #endregion

        private void ContactSelected(object sender, RoutedEventArgs e)
        {
            contactEditor.SetContact(((ListBoxItem)e.Source).Content as Contact);
            contactEditor.Visibility = System.Windows.Visibility.Visible;
        }

        private void SetCurrentBook(ContactBook book)
        {
            _currentBook = book;
            DataContext = book;
            _contactView = CollectionViewSource.GetDefaultView(_currentBook.Contacts);
            OnPropertyChanged(new DependencyPropertyChangedEventArgs(CurrentBookProperty, null, book));
            if (_currentBook.Contacts.Count > 0) lstContacts.SelectedIndex = 0;

            if (contactEditor != null)
            {
                if (_currentBook.Contacts.Count > 0) contactEditor.Visibility = System.Windows.Visibility.Visible;
                else contactEditor.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private bool? CheckForSave()
        {
            if (_currentBook == null) return false;

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
        
        private bool TextPredStart(object item)
        {
            Contact ct = item as Contact;

            return (ct == null || ct.FirstName.ToLower().StartsWith(txtSearch.Text.ToLower()));
        }

        private bool TextPredEmpty(object item)
        {
            return true;
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text)) _contactView.Filter = TextPredEmpty;
            else _contactView.Filter = TextPredStart;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            CheckForSave();
        }

        private void contactEditor_Saved(object sender, EventArgs e)
        {
            _currentBook.Refresh();
            _currentBook.ChangedSinceSave = true;
        }
    }
}
