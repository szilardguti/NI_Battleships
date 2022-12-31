using System.Windows.Input;
using Battleships.ViewModel.Page;

namespace Battleships.ViewModel.Command
{
    public static class NavigationCommandConstants
    {
        private static readonly NavigationCommand _navigateToHistoryPage = new NavigationCommand(HistoryPageViewModel.Instance);
        public static ICommand NavigateToHistoryPage
        {
            get
            {
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

        public static NavigationCommand NavigateToPlayerAgainstPlayerPage
        {
            get
            {
                PlayPageViewModel viewModel = new PlayAgainstPlayerViewModel();

                return new NavigationCommand(viewModel);
            }
        }

        public static NavigationCommand NavigateToPlayerAgainstAIPage
        {
            get
            {
                PlayPageViewModel viewModel = new PlayAgainstAIViewModel();

                return new NavigationCommand(viewModel);
            }
        }
    }
}
