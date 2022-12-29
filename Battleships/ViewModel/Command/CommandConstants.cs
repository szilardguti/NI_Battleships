using Battleships.ViewModel.Page;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Battleships.ViewModel.Command
{
    public static class CommandConstants
    {
        private static readonly NavigationCommand _navigateToHistoryPage = new NavigationCommand(HistoryPageViewModel.Instance);
        public static ICommand NavigateToHistoryPage
        {
            get
            {
                HistoryPageViewModel.Instance.LoadData();

                return _navigateToHistoryPage;
            }
        }

        private static readonly NavigationCommand _navigateToMainPage = new NavigationCommand(MainPageViewModel.Instance);
        public static ICommand NavigateToMainPage
        {
            get
            {
                return _navigateToMainPage;
            }
        }

        private static readonly NavigationCommand _navigateToPlayerAgainstPlayerPage = new NavigationCommand(PlayAgainstPlayerViewModel.Instance);
        public static ICommand NavigateToPlayerAgainstPlayerPage
        {
            get
            {
                return _navigateToPlayerAgainstPlayerPage;
            }
        }

        private static readonly NavigationCommand _navigateToPlayerAgainstAIPage = new NavigationCommand(PlayAgainstAIViewModel.Instance);
        public static ICommand NavigateToPlayerAgainstAIPage
        {
            get
            {
                return _navigateToPlayerAgainstAIPage;
            }
        }
    }
}
