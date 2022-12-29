using System.ComponentModel;
using Battleships.Model;

namespace Battleships.ViewModel.Page
{
    public class PlayAgainstAIViewModel : PlayPageViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private static PlayAgainstAIViewModel _instance;
        private PlayBoardModel _playerModel;
        private PlayBoardModel _aiModel;

        public static PlayPageViewModel Instance
        {
            get
            {
                _instance ??= new PlayAgainstAIViewModel();

                return _instance;
            }
        }

        public PlayAgainstAIViewModel()
        {
            _playerModel = new PlayBoardModel();
            _aiModel = new PlayBoardModel();
        }

        public PlayBoardModel PlayerModel { get { return _playerModel; } }
        public PlayBoardModel AIModel { get { return _aiModel; } }
    }
}
