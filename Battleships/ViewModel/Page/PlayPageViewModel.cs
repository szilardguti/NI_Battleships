using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;
using Battleships.Model;
using Battleships.Model.Helpers;
using Battleships.ViewModel.Command;

namespace Battleships.ViewModel.Page
{
    public abstract class PlayPageViewModel : ViewModelBase
    {
        private string _player1Name;
        public string Player1Name
        {
            get { return _player1Name; }
            set { _player1Name = value; }
        }

        private string _player2Name;
        public string Player2Name
        {
            get { return _player2Name; }
            set { _player2Name = value; }
        }

        private int _rounds;
        public int Rounds { get { return _rounds; } }

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

        private ICommand _mouseDownCommand;
        private ICommand _nextPlayerCommand;

        public ICommand MouseDownCommand { get { return _mouseDownCommand; } }
        public ICommand NextPlayerCommand { get { return _nextPlayerCommand; } }

        protected PlayPageViewModel()
        {
            _mouseDownCommand = new CommandBase(ExecuteTileClick);
            _nextPlayerCommand = new CommandBase(NextPlayer);
            FirstPlayerTileItems = new ObservableCollection<TileItem>();
            SecondPlayerTileItems = new ObservableCollection<TileItem>();
        }

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
    }
}
