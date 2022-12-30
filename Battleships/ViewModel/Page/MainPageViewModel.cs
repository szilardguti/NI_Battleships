using System.ComponentModel;
using System.Windows.Input;
using Battleships.ViewModel.Command;

namespace Battleships.ViewModel.Page
{
    public class MainPageViewModel : ViewModelBase
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private static MainPageViewModel _instance;

        public static MainPageViewModel Instance
        {
            get
            {
                _instance ??= new MainPageViewModel();

                return _instance;
            }
        }

        private string _player1Name;
        private string _player2Name;

        public string Player1Name
        {
            get { return _player1Name; }
            set { _player1Name = value; }
        }

        public string Player2Name
        {
            get { return _player2Name; }
            set { _player2Name = value; }
        }

        private ICommand _navigateToHistoryPage;

        private ICommand _navigateToPlayerAgainstAIPage;

        private ICommand _navigateToPlayerAgainstPlayerPage;

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

        public ICommand NavigateToPlayerAgainstPlayerPage
        {
            get
            {
                return _navigateToPlayerAgainstPlayerPage;
            }
            set
            {
                _navigateToPlayerAgainstPlayerPage = value;
            }
        }

        public MainPageViewModel()
        {
            NavigateToHistoryPage = CommandConstants.NavigateToHistoryPage;
            NavigateToPlayerAgainstAIPage = CommandConstants.NavigateToPlayerAgainstAIPage;
            NavigateToPlayerAgainstPlayerPage = CommandConstants.NavigateToPlayerAgainstPlayerPage;
        }
    }
}
