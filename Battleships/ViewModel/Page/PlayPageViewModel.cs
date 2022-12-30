using System;
using System.Collections.ObjectModel;
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

        public void NextPlayer(object parameter)
        {
            if (!CanShoot)
            {
                if (CurrentPlayer == 1)
                {
                    DrawPlayBoardToCanvas(Player1Model, FirstPlayerTileItems);
                    DrawOtherPlayBoardToCanvas(Player2Model, SecondPlayerTileItems);
                    Rounds += 1;
                    OnPropertyChanged(nameof(Rounds));
                }
                else
                {
                    DrawPlayBoardToCanvas(Player2Model, SecondPlayerTileItems);
                    DrawOtherPlayBoardToCanvas(Player1Model, FirstPlayerTileItems);

                    if (Player2Model.Player.IsARobot)
                    {
                        RobotPlay();
                    }
                }
                CanShoot = true;
            }
        }
        public void ExecuteTileClick(object parameter)
        {
            var mouseArguments = parameter as MouseButtonEventArgs;
            var point = mouseArguments.GetPosition((IInputElement)mouseArguments.Source);

            int xIndex = (int)point.X / 30;
            int yIndex = (int)point.Y / 30;

            if (CanShoot && Winner == 0)
            {
                if (CurrentPlayer == 1)
                {
                    TileStatus status = Player2Model.GetTile(xIndex, yIndex).TileStatus;
                    if (status == TileStatus.Ship)
                    {
                        Tuple<bool, bool> winAndDestroy = Player2Model.Hit(xIndex, yIndex);
                        Winner = winAndDestroy.Item1 ? 1 : 0;

                        Player1Model.Player.HitCount += 1;
                        OnPropertyChanged(nameof(Player1Model.Player.HitCount));

                        if (winAndDestroy.Item2)
                        {
                            OnPropertyChanged(nameof(Player2Model.Player.CarrierCount));
                            OnPropertyChanged(nameof(Player2Model.Player.SubmarineCount));
                            OnPropertyChanged(nameof(Player2Model.Player.DestroyerCount));
                            OnPropertyChanged(nameof(Player2Model.Player.BattleshipCount));
                            OnPropertyChanged(nameof(Player2Model.Player.CruiserCount));
                        }
                    }
                    else if (status == TileStatus.Empty)
                    {
                        Player2Model.Miss(xIndex, yIndex);
                        CurrentPlayer = 2;
                        CanShoot = false;
                    }

                    DrawOtherPlayBoardToCanvas(Player2Model, SecondPlayerTileItems);
                }
                else
                {
                    TileStatus status = Player1Model.GetTile(xIndex, yIndex).TileStatus;
                    if (status == TileStatus.Ship)
                    {
                        Tuple<bool, bool> winAndDestroy = Player1Model.Hit(xIndex, yIndex);
                        Winner = winAndDestroy.Item1 ? 1 : 0;

                        Player2Model.Player.HitCount += 1;
                        OnPropertyChanged(nameof(Player2Model.Player.HitCount));

                        if (winAndDestroy.Item2)
                        {
                            OnPropertyChanged(nameof(Player1Model.Player.CarrierCount));
                            OnPropertyChanged(nameof(Player1Model.Player.BattleshipCount));
                            OnPropertyChanged(nameof(Player1Model.Player.SubmarineCount));
                            OnPropertyChanged(nameof(Player1Model.Player.CruiserCount));
                            OnPropertyChanged(nameof(Player1Model.Player.DestroyerCount));
                        }
                    }
                    else if (status == TileStatus.Empty)
                    {
                        Player1Model.Miss(xIndex, yIndex);
                        CurrentPlayer = 1;
                        CanShoot = false;
                    }

                    DrawOtherPlayBoardToCanvas(Player1Model, FirstPlayerTileItems);
                }
            }
        }
        public abstract void RobotPlay();
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

            if (ReadyToPlay)
            {
                OnPropertyChanged(nameof(PlayElementsVisibility));
                OnPropertyChanged(nameof(NameIOVisibility));

                OnPropertyChanged(nameof(Player1Model.Player.CarrierCount));
                OnPropertyChanged(nameof(Player1Model.Player.BattleshipCount));
                OnPropertyChanged(nameof(Player1Model.Player.SubmarineCount));
                OnPropertyChanged(nameof(Player1Model.Player.CruiserCount));
                OnPropertyChanged(nameof(Player1Model.Player.DestroyerCount));
            }
        }

        public void ExecutePlayer2Submit(object parameter)
        {
            if (RegexMatch(Player2Model.Player.Name))
            {
                Player2Ready = true;
            }

            if (ReadyToPlay)
            {
                OnPropertyChanged(nameof(PlayElementsVisibility));
                OnPropertyChanged(nameof(NameIOVisibility));

                OnPropertyChanged(nameof(Player2Model.Player.CarrierCount));
                OnPropertyChanged(nameof(Player2Model.Player.BattleshipCount));
                OnPropertyChanged(nameof(Player2Model.Player.SubmarineCount));
                OnPropertyChanged(nameof(Player2Model.Player.CruiserCount));
                OnPropertyChanged(nameof(Player2Model.Player.DestroyerCount));
            }
        }

        private bool RegexMatch(string playerName)
        {
            return Regex.IsMatch(playerName, @"^[a-zA-Z0-9]+$");
        }
    }
}
