using Battleships.ViewModel.Command;
using Battleships.ViewModel.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace Battleships.ViewModel.Page
{
    public class MainPageViewModel : ViewModelBase
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private static MainPageViewModel _instance;

        public static ViewModelBase Instance
        {
            get
            {
                _instance ??= new MainPageViewModel();

                return _instance;
            }
        }
    }
}
