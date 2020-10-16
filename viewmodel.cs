using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Threading;

namespace WpfApp1
{


    public class viewmodel : INotifyPropertyChanged
    {

        private ObservableCollection<myItem> collection;

        public ObservableCollection<myItem> Collection
        {
            get { return collection; }
            set
            {
                collection = value;
                OnPropertyChanged("Collection");
            }
        }


        public viewmodel()
        {
            Collection = new ObservableCollection<myItem>();
            myItem item1 = new myItem {Name = "name1", Status = "OK"};
            myItem item2 = new myItem {Name = "name2", Status = "ERROR"};
            DispatchService.Invoke(() =>
            {
                Collection.Add(item1);
                Collection.Add(item2);
            });
        }


        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion


    }

    public class myItem
    {
        public string Status { get; set; }
        public string Name { get; set; }
    }

    public static class DispatchService
    {
        public static void Invoke(Action action)
        {
            Dispatcher dispatchObject = Application.Current.Dispatcher;
            if (dispatchObject == null || dispatchObject.CheckAccess())
            {
                action();
            }
            else
            {
                dispatchObject.Invoke(action);
            }
        }
    }

   
}