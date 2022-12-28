using Battleships.ViewModel.Navigation;
using System.ComponentModel;

namespace Battleships.ViewModel
{
    public class AppWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private IPageViewModel _currentPageViewModel;
        public IPageViewModel CurrentPageViewModel
        {
            get { return _currentPageViewModel; }
            set { _currentPageViewModel = value; }
        }

        public AppWindowViewModel()
        {
            CurrentPageViewModel = NavigationModule.Instance.CurrentPageViewModel;
        }
    }
}
