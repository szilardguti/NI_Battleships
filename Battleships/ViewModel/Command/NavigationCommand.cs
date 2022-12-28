using Battleships.ViewModel.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Battleships.ViewModel.Command
{
    public class NavigationCommand : CommandBase
    {
        private readonly ViewModelBase _destinationPage;

        public NavigationCommand(ViewModelBase destination)
        {
            _destinationPage = destination;
        }

        public override void Execute(object parameter)
        {
            NavigationModule.Instance.ChangeViewModel(_destinationPage);
        }
    }
}
