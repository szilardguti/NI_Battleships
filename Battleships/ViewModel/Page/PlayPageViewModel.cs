using System;
using System.Collections.Generic;
using System.Text;

namespace Battleships.ViewModel.Page
{
    public abstract class PlayPageViewModel : ViewModelBase
    {
        private string _player1Name;
        public string Player1Name { get { return _player1Name; } }

        private string _player2Name;
        public string Player2Name { get { return _player2Name; } }

        private int _rounds;
        public int Rounds { get { return _rounds; } }
    }
}
