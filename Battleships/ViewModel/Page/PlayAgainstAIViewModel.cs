using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Battleships.ViewModel.Page
{
    public class PlayAgainstAIViewModel : PlayPageViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private static PlayAgainstAIViewModel _instance;

        public static PlayPageViewModel Instance
        {
            get
            {
                _instance ??= new PlayAgainstAIViewModel();

                return _instance;
            }
        }
    }
}
