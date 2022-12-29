using Battleships.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

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

        private Collection<GameResult> _listOfResults;

        public Collection<GameResult> ListOfResults
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
            // Load in list of database entities
            // TODO
            Collection<GameResult> collection = new Collection<GameResult>();

            ListOfResults = collection;

            OnPropertyChanged(nameof(ListOfResults));
        }
    }
}
