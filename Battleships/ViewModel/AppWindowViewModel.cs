using Battleships.ViewModel.Navigation;

namespace Battleships.ViewModel
{
    public class AppWindowViewModel : ViewModelBase
    {
        public ViewModelBase CurrentPage
        {
            get { return NavigationModule.Instance.CurrentViewModel; }
        }

        public AppWindowViewModel()
        {
            NavigationModule.Instance.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentPage));
        }
    }
}
