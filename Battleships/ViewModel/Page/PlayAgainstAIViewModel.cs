using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Battleships.ViewModel.Page
{
    public class PlayAgainstAIViewModel : ViewModelBase
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private static PlayAgainstAIViewModel _instance;

        public static ViewModelBase Instance
        {
            get
            {
                _instance ??= new PlayAgainstAIViewModel();

                return _instance;
            }
        }
    }
}
