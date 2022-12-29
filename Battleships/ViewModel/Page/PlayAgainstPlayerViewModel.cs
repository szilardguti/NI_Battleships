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
    }
}
