using System;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace AddressBook
{
    [Serializable]
    public class RefreshableCollection<T> : ObservableCollection<T>
    {
        public void Refresh()
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
}
