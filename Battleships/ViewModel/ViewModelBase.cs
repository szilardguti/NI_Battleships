using Battleships.ViewModel.Page;
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

        public static ViewModelBase Instance { get; set; }
    }
}
