﻿using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Battleships.Model;
using Battleships.Model.Helpers;
using Battleships.ViewModel.Command;

namespace Battleships.ViewModel.Page
{
    public abstract class PlayPageViewModel : ViewModelBase
    {
        protected PlayPageViewModel()
        {
            _player1Model = new PlayBoardModel();
            _player2Model = new PlayBoardModel();
            _mouseDownCommand = new CommandBase(ExecuteTileClick);
            _nextPlayerCommand = new CommandBase(NextPlayer);
            _submitPlayer1Name = new CommandBase(ExecutePlayer1Submit);
            _submitPlayer2Name = new CommandBase(ExecutePlayer2Submit);
            FirstPlayerTileItems = new ObservableCollection<TileItem>();
            SecondPlayerTileItems = new ObservableCollection<TileItem>();
            DrawPlayBoardToCanvas(Player1Model, FirstPlayerTileItems);
            DrawOtherPlayBoardToCanvas(Player2Model, SecondPlayerTileItems);
        }

        private bool _player1Ready;
        private bool _player2Ready;

        public bool Player1Ready
        {
            get
            {
                return _player1Ready;
            }

            set
            {
                _player1Ready = value;
            }
        }

        public bool Player2Ready
        {
            get
            {
                return _player2Ready;
            }

            set
            {
                _player2Ready = value;
            }
        }

        private bool _readyToPlay = false;

        public bool ReadyToPlay
        {
            get
            {
                if (_player1Ready && _player2Ready)
                {
                    _readyToPlay = true;
                }

                return _readyToPlay;
            }
        }

        public Visibility PlayElementsVisibility
        {
            get
            {
                return ReadyToPlay ? Visibility.Visible : Visibility.Hidden;
            }
        }

        public Visibility NameIOVisibility
        {
            get
            {
                return ReadyToPlay ? Visibility.Hidden : Visibility.Visible;
            }
        }

        private int _rounds = 1;
        public int Rounds { get { return _rounds; } set { _rounds = value; } }

        private int _winner = 0;
        private int _currentPlayer = 1;
        private bool _canShoot = true;
        public int Winner
        {
            get { return _winner; }
            set { _winner = value; }
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

        public ObservableCollection<TileItem> FirstPlayerTileItems { get; set; }
        public ObservableCollection<TileItem> SecondPlayerTileItems { get; set; }

        private readonly ICommand _mouseDownCommand;
        private readonly ICommand _nextPlayerCommand;

        public ICommand MouseDownCommand { get { return _mouseDownCommand; } }
        public ICommand NextPlayerCommand { get { return _nextPlayerCommand; } }

        private readonly PlayBoardModel _player1Model;
        private readonly PlayBoardModel _player2Model;

        public PlayBoardModel Player1Model { get { return _player1Model; } }
        public PlayBoardModel Player2Model { get { return _player2Model; } }

        public abstract void NextPlayer(object parameter);
        public abstract void ExecuteTileClick(object parameter);
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

        public void DrawOtherPlayBoardToCanvas(PlayBoardModel playBoard, ObservableCollection<TileItem> tileItems)
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
                        TileStatus.Destroyed => Brushes.Red,
                        _ => Brushes.White,
                    }
                };
                tileItems.Add(newTileItem);
            }
        }

        private readonly ICommand _submitPlayer1Name;
        private readonly ICommand _submitPlayer2Name;

        public ICommand SubmitPlayer1Name { get { return _submitPlayer1Name; } }
        public ICommand SubmitPlayer2Name { get { return _submitPlayer2Name; } }

        public void ExecutePlayer1Submit(object parameter)
        {
            if (RegexMatch(Player1Model.Player.Name))
            {
                Player1Ready = true;
            }

            OnPropertyChanged(nameof(PlayElementsVisibility));
            OnPropertyChanged(nameof(NameIOVisibility));
        }

        public void ExecutePlayer2Submit(object parameter)
        {
            if (RegexMatch(Player2Model.Player.Name))
            {
                Player2Ready = true;
            }

            OnPropertyChanged(nameof(PlayElementsVisibility));
            OnPropertyChanged(nameof(NameIOVisibility));
        }

        private bool RegexMatch(string playerName)
        {
            return Regex.IsMatch(playerName, @"^[a-zA-Z0-9]+$");
        }
    }
}
