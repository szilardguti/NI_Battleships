using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media;
using Battleships.Model;
using Battleships.Model.Helpers;

namespace Battleships.ViewModel.Page
{
    public class PlayAgainstAIViewModel : PlayPageViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private static PlayAgainstAIViewModel _instance;
        private PlayBoardModel _playerModel;
        private PlayBoardModel _aiModel;

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
            PlayBoard = new PlayBoardModel();
            FirstPlayerTileItems= new ObservableCollection<TileItem>();
            DrawPlayBoardToCanvas(PlayBoard, FirstPlayerTileItems);
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

        public PlayBoardModel PlayBoard { get; set; }
    }
}
