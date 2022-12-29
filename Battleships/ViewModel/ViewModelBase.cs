using Battleships.Model.Helpers;
using Battleships.ViewModel.Page;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Battleships.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
