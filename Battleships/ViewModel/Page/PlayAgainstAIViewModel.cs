using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Battleships.Model;
using Battleships.Model.Helpers;
using Battleships.ViewModel.Command;

namespace Battleships.ViewModel.Page
{
    public class PlayAgainstAIViewModel : PlayPageViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private static PlayAgainstAIViewModel _instance;
        private PlayBoardModel _playerModel;
        private PlayBoardModel _aiModel;
        private ICommand _mouseDownCommand;

        public ObservableCollection<TileItem> FirstPlayerTileItems { get; set; }

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

            _mouseDownCommand = new CommandBase(ExecuteTileClick);
            FirstPlayerTileItems = new ObservableCollection<TileItem>();
            DrawPlayBoardToCanvas(PlayerModel, FirstPlayerTileItems);
        }

        private void DrawPlayBoardToCanvas(PlayBoardModel playBoard, ObservableCollection<TileItem> tileItems)
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
                        TileStatus.MissShot => Brushes.Yellow,
                        TileStatus.HitShot => Brushes.Red,
                        _ => Brushes.White,
                    }
                };
                tileItems.Add(newTileItem);
            }
        }

        public PlayBoardModel PlayerModel { get { return _playerModel; } }
        public PlayBoardModel AIModel { get { return _aiModel; } }
        public ICommand MouseDownCommand { get { return _mouseDownCommand; } }

        public void ExecuteTileClick(object parameter)
        {
            var mouseArguments = parameter as MouseButtonEventArgs;
            var point = mouseArguments.GetPosition((IInputElement)mouseArguments.Source);

            int xIndex = (int)point.X / 30;
            int yIndex = (int)point.Y / 30;

            if (PlayerModel.GetTile(xIndex, yIndex).TileStatus == TileStatus.Ship)
            {
                PlayerModel.SetTile(xIndex, yIndex, TileStatus.HitShot);
            }
            else
            {
                PlayerModel.SetTile(xIndex, yIndex, TileStatus.MissShot);
            }

            DrawPlayBoardToCanvas(PlayerModel, FirstPlayerTileItems);
        }
    }
}
