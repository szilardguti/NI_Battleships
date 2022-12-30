using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Battleships.DAL;
using Battleships.DAL.Entities;

namespace Battleships.ViewModel.Page
{
    public class HistoryPageViewModel : ViewModelBase
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private static HistoryPageViewModel _instance;

        public static HistoryPageViewModel Instance
        {
            get
            {
                _instance ??= new HistoryPageViewModel();

                return _instance;
            }
        }

        private HistoryPageViewModel()
        {
            ResultRepository = new ResultRepository();
        }

        private ResultRepository ResultRepository { get; set; }

        private ICollection<GameResult> _listOfResults;

        public ICollection<GameResult> ListOfResults
        {
            get
            {
                _listOfResults ??= new Collection<GameResult>();

                return _listOfResults;
            }

            set
            {
                _listOfResults = value;
            }
        }

        public void LoadData()
        {
            ListOfResults = ResultRepository.GetAllGames();

            OnPropertyChanged(nameof(ListOfResults));
        }
    }
}
