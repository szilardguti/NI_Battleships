using Battleships.ViewModel.Page;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Battleships.ViewModel.Command
{
    public static class CommandConstants
    {
        private static readonly NavigationCommand _navigateToHistoryPage = new NavigationCommand(HistoryPageViewModel.Instance);
        public static readonly ICommand NavigateToHistoryPage = _navigateToHistoryPage;
    }
}
