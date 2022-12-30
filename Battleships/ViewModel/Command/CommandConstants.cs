using System.Windows.Input;
using Battleships.ViewModel.Page;

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

        public static NavigationCommand NavigateToPlayerAgainstPlayerPage
        {
            get
            {
                PlayPageViewModel viewModel = new PlayAgainstPlayerViewModel(
                    MainPageViewModel.Instance.Player1Name,
                    MainPageViewModel.Instance.Player2Name);

                return new NavigationCommand(viewModel);
            }
        }

        public static NavigationCommand NavigateToPlayerAgainstAIPage
        {
            get
            {
                PlayPageViewModel viewModel = new PlayAgainstAIViewModel(MainPageViewModel.Instance.Player1Name);

                return new NavigationCommand(viewModel);
            }
        }
    }
}
