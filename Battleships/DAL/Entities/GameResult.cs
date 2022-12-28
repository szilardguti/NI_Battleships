using System;
using System.Collections.Generic;
using System.Text;

namespace Battleships.DAL.Entities
{
    public class GameResult
    {
        public string FirstPlayerName { get; set; }

        public string SecondPlayerName { get; set; }

        public int FirstPlayerHitCount { get; set; }

        public int SecondPlayerHitCount { get; set; }

        public int RoundCount { get; set; }

        public bool IsInProgress { get; set; }

        public string WinnerName { get; set; }

        public ICollection<GameActions> Actions { get; set; }
    }
}
