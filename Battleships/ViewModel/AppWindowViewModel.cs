using Battleships.ViewModel.Command;
using Battleships.ViewModel.Navigation;
using Battleships.ViewModel.Page;
using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Navigation;

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
