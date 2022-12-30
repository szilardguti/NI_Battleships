using Battleships.ViewModel.Command;
using Battleships.ViewModel.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace Battleships.ViewModel.Page
{
    public class MainPageViewModel : ViewModelBase
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private static MainPageViewModel _instance;

        public static ViewModelBase Instance
        {
            get
            {
                _instance ??= new MainPageViewModel();

                return _instance;
            }
        }

        private ICommand _navigateToHistoryPage;

        private ICommand _navigateToPlayerAgainstAIPage;

        public ICommand NavigateToHistoryPage
        {
            get
            {
                return _navigateToHistoryPage;
            }
            set
            {
                _navigateToHistoryPage = value;
            }
        }

        public ICommand NavigateToPlayerAgainstAIPage
        {
            get
            {
                return _navigateToPlayerAgainstAIPage;
            }
            set
            {
                _navigateToPlayerAgainstAIPage = value;
            }
        }

        public MainPageViewModel()
        {
            NavigateToHistoryPage = CommandConstants.NavigateToHistoryPage;
            NavigateToPlayerAgainstAIPage = CommandConstants.NavigateToPlayerAgainstAIPage;
        }
    }
}
