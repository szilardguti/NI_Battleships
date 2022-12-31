using System;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Battleships.DAL;
using Battleships.DAL.Entities;
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
            _showAIBoard = new CommandBase(ExecuteShowAIBoard);
            FirstPlayerTileItems = new ObservableCollection<TileItem>();
            SecondPlayerTileItems = new ObservableCollection<TileItem>();
            ResultRepository = new ResultRepository();
            DrawPlayBoardToCanvas(Player1Model, FirstPlayerTileItems);
            DrawOtherPlayBoardToCanvas(Player2Model, SecondPlayerTileItems);
        }

        private bool _player1Ready;
        private bool _player2Ready;
        private bool _aiShowing = false;

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

        private bool _player1FormatError;
        private bool _player2FormatError;

        public Visibility Player1FormatErrorVisibility
        {
            get
            {
                return _player1FormatError ? Visibility.Visible : Visibility.Hidden;
            }
        }
        public Visibility Player2FormatErrorVisibility
        {
            get
            {
                return _player2FormatError ? Visibility.Visible : Visibility.Hidden;
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

        public string WinnerName
        {
            get
            {
                string winnerName = string.Empty;

                switch (Winner)
                {
                    case 1:
                        winnerName = Player1Model.Player.Name;
                        break;
                    case 2:
                        winnerName = Player2Model.Player.Name;
                        break;
                    case 0:
                        return winnerName;
                }

                return winnerName;
            }
        }

        public Visibility WinPanelVisibility
        {
            get
            {
                switch (Winner)
                {
                    case 1: case 2: return Visibility.Visible;
                    default: return Visibility.Hidden;
                }
            }
        }

        public ObservableCollection<TileItem> FirstPlayerTileItems { get; set; }
        public ObservableCollection<TileItem> SecondPlayerTileItems { get; set; }

        private readonly ICommand _mouseDownCommand;
        private readonly ICommand _nextPlayerCommand;

        public ICommand MouseDownCommand { get { return _mouseDownCommand; } }
        public ICommand NextPlayerCommand { get { return _nextPlayerCommand; } }

        private readonly PlayBoardModel _player1Model;
        private readonly PlayBoardModel _player2Model;

        public GameResult MatchResult { get; set; }

        private IResultRepository ResultRepository { get; set; }

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
                AddActionToDatabase(Player1Model, Player2Model, xIndex, yIndex);
                if (CurrentPlayer == 1)
                {
                    TileStatus status = Player2Model.GetTile(xIndex, yIndex).TileStatus;
                    if (status == TileStatus.Ship)
                    {
                        Tuple<bool, bool> winAndDestroy = Player2Model.Hit(xIndex, yIndex);
                        Winner = winAndDestroy.Item1 ? 1 : 0;

                        Player1Model.Player.HitCount += 1;

                        UpdatePlayer2Properties();

                        UpdateWinStateProperties();
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
                        Winner = winAndDestroy.Item1 ? 2 : 0;

                        Player2Model.Player.HitCount += 1;

                        UpdatePlayer1Properties();

                        UpdateWinStateProperties();
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
            UpdateGameResultInDatabase(Player1Model, Player2Model);
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
        private readonly ICommand _showAIBoard;

        public ICommand ShowAIBoard { get { return _showAIBoard; } }
        public ICommand SubmitPlayer1Name { get { return _submitPlayer1Name; } }
        public ICommand SubmitPlayer2Name { get { return _submitPlayer2Name; } }

        public void ExecutePlayer1Submit(object parameter)
        {
            if (RegexMatch(Player1Model.Player.Name))
            {
                Player1Ready = true;
                _player1FormatError = false;
            }
            else
            {
                _player1FormatError = true;
            }

            OnPropertyChanged(nameof(Player1FormatErrorVisibility));

            if (ReadyToPlay)
            {
                StartGame();
            }
        }

        private void StartGame()
        {
            Player1Model.Player.DestroyerCount = 1;
            Player1Model.Player.SubmarineCount = 1;
            Player1Model.Player.CarrierCount = 1;
            Player1Model.Player.BattleshipCount = 1;
            Player1Model.Player.CruiserCount = 1;

            Player2Model.Player.DestroyerCount = 1;
            Player2Model.Player.SubmarineCount = 1;
            Player2Model.Player.CarrierCount = 1;
            Player2Model.Player.BattleshipCount = 1;
            Player2Model.Player.CruiserCount = 1;

            OnPropertyChanged(nameof(PlayElementsVisibility));
            OnPropertyChanged(nameof(NameIOVisibility));

            UpdatePlayer1Properties();
            UpdatePlayer2Properties();

            MatchResult = CreateGameResultInDatabase(Player1Model, Player2Model);
        }

        private GameResult CreateGameResultInDatabase(PlayBoardModel player1Model, PlayBoardModel player2Model)
        {
            GameResult newGameResult = new GameResult()
            {
                FirstPlayerName = player1Model.Player.Name,
                FirstPlayerHitCount = player1Model.Player.HitCount,
                SecondPlayerName = player2Model.Player.Name,
                SecondPlayerHitCount = player2Model.Player.HitCount,
                RoundCount = Rounds,
                IsInProgress = Winner == 0,
                WinnerName = string.Empty,
            };

            return ResultRepository.CreateGameResult(newGameResult);
        }

        private void UpdateGameResultInDatabase(PlayBoardModel player1Model, PlayBoardModel player2Model)
        {
            MatchResult.FirstPlayerName = player1Model.Player.Name;
            MatchResult.FirstPlayerHitCount = player1Model.Player.HitCount;
            MatchResult.SecondPlayerName = player2Model.Player.Name;
            MatchResult.SecondPlayerHitCount = player2Model.Player.HitCount;
            MatchResult.RoundCount = Rounds;
            MatchResult.IsInProgress = Winner == 0;
            if (!MatchResult.IsInProgress)
            {
                MatchResult.WinnerName = Winner == 1 ? MatchResult.FirstPlayerName : MatchResult.SecondPlayerName;
            }
            else
            {
                MatchResult.WinnerName = string.Empty;
            }

            ResultRepository.UpdateGameResult(MatchResult);
        }

        private void AddActionToDatabase(PlayBoardModel player1Model, PlayBoardModel player2Model, int xIndex, int yIndex)
        {
            GameAction gameAction = new GameAction()
            {
                PlayerName = CurrentPlayer == 1 ? player1Model.Player.Name : player2Model.Player.Name,
                FirstPlayerBoardStatus = player1Model.Tiles,
                SecondPlayerBoardStatus = player2Model.Tiles,
                FirstPlayerShips = player1Model.Ships,
                SecondPlayerShips = player2Model.Ships,
                GameResult = MatchResult,
            };

            string firstChar = xIndex switch
            {
                0 => "A",
                1 => "B",
                2 => "C",
                3 => "D",
                4 => "E",
                5 => "F",
                6 => "G",
                7 => "H",
                8 => "I",
                9 => "J",
                _ => "-",
            };
            gameAction.ActionString = string.Concat(firstChar, yIndex + 1);

            ResultRepository.AddActionToGame(gameAction);
        }

        public void ExecutePlayer2Submit(object parameter)
        {
            if (RegexMatch(Player2Model.Player.Name))
            {
                Player2Ready = true;
                _player2FormatError = false;
            }
            else
            {
                _player2FormatError = true;
            }

            OnPropertyChanged(nameof(Player2FormatErrorVisibility));

            if (ReadyToPlay)
            {
                StartGame();
            }
        }

        public void ExecuteShowAIBoard(object parameter)
        {
            if (CurrentPlayer == 1 && !_aiShowing)
            {
                DrawPlayBoardToCanvas(Player2Model, SecondPlayerTileItems);
                _aiShowing = true;
            }
            else if (CurrentPlayer == 1 && _aiShowing)
            {
                DrawOtherPlayBoardToCanvas(Player2Model, SecondPlayerTileItems);
                _aiShowing = false;
            }
        }

        private bool RegexMatch(string playerName)
        {
            return Regex.IsMatch(playerName, @"^[a-zA-Z0-9]+$");
        }

        private void UpdatePlayer1Properties()
        {
            OnPropertyChanged(nameof(Player1Model));
        }

        private void UpdatePlayer2Properties()
        {
            OnPropertyChanged(nameof(Player2Model));
        }

        private void UpdateWinStateProperties()
        {
            OnPropertyChanged(nameof(WinnerName));
            OnPropertyChanged(nameof(WinPanelVisibility));
        }
    }
}
