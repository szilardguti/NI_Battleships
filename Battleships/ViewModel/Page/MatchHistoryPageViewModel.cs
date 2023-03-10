using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Battleships.DAL;
using Battleships.DAL.Entities;
using Battleships.Model;
using Battleships.Model.Helpers;
using Battleships.ViewModel.Command;

namespace Battleships.ViewModel.Page
{
    public class MatchHistoryPageViewModel : ViewModelBase
    {
        public GameResult HistoryGameResult { get; set; }

        public ICollection<GameAction> GameActions { get; set; }

        public PlayBoardModel Player1Model { get; set; }
        public PlayBoardModel Player2Model { get; set; }
        public ObservableCollection<TileItem> FirstPlayerTileItems { get; set; }
        public ObservableCollection<TileItem> SecondPlayerTileItems { get; set; }

        private ResultRepository ResultRepository { get; set; }

        private readonly ICommand _selectActionInHistory;
        public ICommand SelectActionInHistory { get { return _selectActionInHistory; } }

        public MatchHistoryPageViewModel()
        {
            _selectActionInHistory = new CommandBase(LoadSelectedAction);

            HistoryGameResult = HistoryPageViewModel.PickedGameHistory;
            SetupPlayerModels();

            ResultRepository = new ResultRepository();
            GameActions = ResultRepository.GetAllActions(HistoryGameResult.Id);

            FirstPlayerTileItems = new ObservableCollection<TileItem>();
            SecondPlayerTileItems = new ObservableCollection<TileItem>();

            ShowAction(GameActions.Last());
        }

        private void SetupPlayerModels()
        {
            Player1Model = new PlayBoardModel();
            Player2Model = new PlayBoardModel();

            Player1Model.Player.Name = HistoryGameResult.FirstPlayerName;
            Player2Model.Player.Name = HistoryGameResult.SecondPlayerName;

            UpdatePlayerProperties();
        }

        private void ShowAction(GameAction action)
        {
            Player1Model.Tiles = new Collection<Tile>(action.FirstPlayerBoardStatus.ToList());
            Player2Model.Tiles = new Collection<Tile>(action.SecondPlayerBoardStatus.ToList());

            DrawPlayBoardToCanvas(Player1Model, FirstPlayerTileItems);
            DrawPlayBoardToCanvas(Player2Model, SecondPlayerTileItems);
        }

        private void UpdatePlayerProperties()
        {
            OnPropertyChanged(nameof(Player1Model));
            OnPropertyChanged(nameof(Player2Model));
        }

        private void LoadSelectedAction(object parameter)
        {
            if (parameter is GameAction action)
            {
                ShowAction(action);
            }
        }

        public void DrawPlayBoardToCanvas(PlayBoardModel playBoard, ObservableCollection<TileItem> tileItems)
        {
            tileItems.Clear();

            foreach (Tile tile in playBoard.Tiles)
            {
                TileItem newTileItem = new TileItem
                {
                    X = tile.X * 30,
                    Y = tile.Y * 30,
                    Height = 30,
                    Width = 30,
                    Color = tile.TileStatus switch
                    {
                        TileStatus.Empty => Brushes.Aqua,
                        TileStatus.Ship => Brushes.DarkGray,
                        TileStatus.MissShot => Brushes.Black,
                        TileStatus.HitShot => Brushes.Yellow,
                        TileStatus.Destroyed => Brushes.Red,
                        _ => Brushes.White,
                    }
                };
                tileItems.Add(newTileItem);
            }
        }
    }
}
