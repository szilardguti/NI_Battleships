using Battleships.Model.Helpers;
using Battleships.ViewModel.Page;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Battleships.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Collection<Tile> _tiles;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Collection<Tile> Tiles { get { return _tiles; } }

        public void initGame()
        {
            _tiles = new Collection<Tile>();
            for(int i = 0; i < Constants.PlayerBoardSize; i++)
            {
                for(int j=0;j< Constants.PlayerBoardSize; j++)
                {
                    _tiles.Add(new Tile()
                    {
                        X = i,
                        Y = j,
                        TileStatus = Model.TileStatus.Empty
                    });
                }
            }
        }
    }
}
