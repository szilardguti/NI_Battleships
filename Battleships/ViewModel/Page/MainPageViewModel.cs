using System.ComponentModel;
using System.Windows.Input;
using Battleships.ViewModel.Command;

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
