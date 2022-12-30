using Battleships.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Battleships.ViewModel.Page
{
    public class PlayAgainstPlayerViewModel : PlayPageViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private static PlayAgainstPlayerViewModel _instance;

        public static PlayPageViewModel Instance
        {
            get
            {
                _instance ??= new PlayAgainstPlayerViewModel();

                return _instance;
            }
        }

        public override void NextPlayer(object parameter)
        {
            throw new NotImplementedException();
        }

        public override void ExecuteTileClick(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
