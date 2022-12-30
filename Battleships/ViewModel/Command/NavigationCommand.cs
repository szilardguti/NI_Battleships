using Battleships.ViewModel.Navigation;

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
