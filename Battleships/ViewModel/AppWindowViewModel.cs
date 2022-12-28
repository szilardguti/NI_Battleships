using Battleships.ViewModel.Navigation;
using System.ComponentModel;

namespace Battleships.ViewModel
{
    public class AppWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public IPageViewModel CurrentPage
        {
            get { return NavigationModule.Instance.CurrentViewModel; }
        }
    }
}
