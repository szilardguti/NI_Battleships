using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using Battleships.DAL;
using Battleships.DAL.Entities;
using Battleships.ViewModel.Command;

namespace Battleships.ViewModel.Page
{
    public class HistoryPageViewModel : ViewModelBase
    {
        private static HistoryPageViewModel _instance;
        public static HistoryPageViewModel Instance
        {
            get
            {
                _instance ??= new HistoryPageViewModel();

                return _instance;
            }
        }

        public HistoryPageViewModel()
        {
            _mouseDownCommand = new CommandBase(NavigateToMatchHistoryWithId);
            ResultRepository = new ResultRepository();
            LoadData();
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

        public static GameResult PickedGameHistory { get; set; }

        private readonly ICommand _mouseDownCommand;

        public ICommand MouseDownCommand { get { return _mouseDownCommand; } }

        public void LoadData()
        {
            ListOfResults = ResultRepository.GetAllGames();

            OnPropertyChanged(nameof(ListOfResults));
        }

        private void NavigateToMatchHistoryWithId(object parameter)
        {
            if (parameter is GameResult result)
            {
                PickedGameHistory = result;

                MatchHistoryPageViewModel viewModel = new MatchHistoryPageViewModel();

                NavigationCommand navigationCommand = new NavigationCommand(viewModel);
                navigationCommand.Execute(viewModel);
            }
        }
    }
}
