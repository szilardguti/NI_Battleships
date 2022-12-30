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
        private ICommand _nextPlayerCommand;

        private int _currentPlayer = 1;
        private bool _canShoot = true;

        public ObservableCollection<TileItem> FirstPlayerTileItems { get; set; }
        public ObservableCollection<TileItem> SecondPlayerTileItems { get; set; }

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
            _nextPlayerCommand = new CommandBase(NextPlayer);
            FirstPlayerTileItems = new ObservableCollection<TileItem>();
            SecondPlayerTileItems = new ObservableCollection<TileItem>();
            DrawPlayBoardToCanvas(PlayerModel, FirstPlayerTileItems);
            DrawOtherPlayBoardToCanvas(AIModel, SecondPlayerTileItems);
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
                        TileStatus.MissShot => Brushes.Black,
                        TileStatus.HitShot => Brushes.Yellow,
                        TileStatus.Destroyed=> Brushes.Red,
                        _ => Brushes.White,
                    }
                };
                tileItems.Add(newTileItem);
            }
        }

        private void DrawOtherPlayBoardToCanvas(PlayBoardModel playBoard, ObservableCollection<TileItem> tileItems)
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
                        TileStatus.Ship => Brushes.Aqua,
                        TileStatus.MissShot => Brushes.Black,
                        TileStatus.HitShot => Brushes.Yellow,
                        TileStatus.Destroyed=> Brushes.Red,
                        _ => Brushes.White,
                    }
                };
                tileItems.Add(newTileItem);
            }
        }

        public int CurrentPlayer
        { 
            get { return _currentPlayer; }
            set { _currentPlayer = value; }
        }

        public bool CanShoot
        {
            get { return _canShoot; }
            set { _canShoot = value; }
        }
        public PlayBoardModel PlayerModel { get { return _playerModel; } }
        public PlayBoardModel AIModel { get { return _aiModel; } }
        public ICommand MouseDownCommand { get { return _mouseDownCommand; } }
        public ICommand NextPlayerCommand { get { return _nextPlayerCommand; } }

        public void ExecuteTileClick(object parameter)
        {
            var mouseArguments = parameter as MouseButtonEventArgs;
            var point = mouseArguments.GetPosition((IInputElement)mouseArguments.Source);

            int xIndex = (int)point.X / 30;
            int yIndex = (int)point.Y / 30;

            if (CanShoot)
            {

                if (CurrentPlayer == 1)
                {
                    TileStatus status = AIModel.GetTile(xIndex, yIndex).TileStatus;
                    if (status == TileStatus.Ship)
                    {
                        AIModel.Hit(xIndex, yIndex);
                    }
                    else if (status == TileStatus.Empty)
                    {
                        AIModel.Miss(xIndex, yIndex);
                        CurrentPlayer = 2;
                        CanShoot = false;
                    }

                    DrawOtherPlayBoardToCanvas(AIModel, SecondPlayerTileItems);
                }
                else
                {
                    TileStatus status = PlayerModel.GetTile(xIndex, yIndex).TileStatus;
                    if (status == TileStatus.Ship)
                    {
                        PlayerModel.Hit(xIndex, yIndex);
                    }
                    else if (status == TileStatus.Empty)
                    {
                        PlayerModel.Miss(xIndex, yIndex);
                        CurrentPlayer = 1;
                        CanShoot = false;
                    }

                    DrawOtherPlayBoardToCanvas(PlayerModel, FirstPlayerTileItems);
                }
            }
        }

        public void NextPlayer(object parameter)
        {
            if (!CanShoot)
            {
                if (CurrentPlayer == 1)
                {
                    DrawPlayBoardToCanvas(PlayerModel, FirstPlayerTileItems);
                    DrawOtherPlayBoardToCanvas(AIModel, SecondPlayerTileItems);
                }
                else
                {
                    DrawPlayBoardToCanvas(AIModel, SecondPlayerTileItems);
                    DrawOtherPlayBoardToCanvas(PlayerModel, FirstPlayerTileItems);
                }
                CanShoot = true;
            }
        }
    }
}
